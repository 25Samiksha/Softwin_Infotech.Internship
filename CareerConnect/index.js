const express = require("express");
const sql = require("./db");
const session = require("express-session");

const app = express();


// ==============================
// SETTINGS
// ==============================

app.set("view engine", "ejs");

app.use(express.static("public"));

app.use(express.urlencoded({ extended: true }));


// ==============================
// SESSION
// ==============================

app.use(session({
    secret: "careerconnect-secret-key",
    resave: false,
    saveUninitialized: false
}));


// ==============================
// HOME PAGE
// ==============================

app.get("/", (request, response) => {

    const student = {
        name: "Samiksha",
        course: "B.Tech Computer Science",
        city: "Sangli"
    };

    response.render("home", {
        student: student
    });

});


// ==============================
// JOBS PAGE
// ==============================

app.get("/jobs", async (request, response) => {

    try {

        const result = await sql.query`

            SELECT *

            FROM Jobs

            ORDER BY PostedDate DESC

        `;

        console.log("Jobs fetched:");

        console.log(result.recordset);


        response.render("jobs", {

            jobs: result.recordset

        });

    }
    catch (error) {

        console.log("Jobs error:");

        console.log(error);


        response.send(
            "Something went wrong while loading jobs."
        );

    }

});


// ==============================
// APPLY FOR JOB
// ==============================

app.post("/apply/:jobId", async (request, response) => {

    if (!request.session.userId) {

        return response.redirect("/login");

    }


    // Only students can apply

    if (request.session.role !== "Student") {

        return response.send(
            "Only students can apply for jobs."
        );

    }


    const userId = request.session.userId;

    const jobId = request.params.jobId;


    try {

        // Check whether job exists

        const checkJob = await sql.query`

            SELECT *

            FROM Jobs

            WHERE JobId = ${jobId}

        `;


        if (checkJob.recordset.length === 0) {

            return response.send(
                "Job not found."
            );

        }


        // Check whether user already applied

        const checkApplication = await sql.query`

            SELECT *

            FROM Applications

            WHERE UserId = ${userId}

            AND JobId = ${jobId}

        `;


        if (checkApplication.recordset.length > 0) {

            return response.send(
                "You have already applied for this job."
            );

        }


        // Insert application

        await sql.query`

            INSERT INTO Applications
            (
                UserId,
                JobId,
                Status
            )

            VALUES
            (
                ${userId},
                ${jobId},
                'Pending'
            )

        `;


        console.log("Application submitted!");

        console.log("User ID:", userId);

        console.log("Job ID:", jobId);


        response.send(
            "Application submitted successfully!"
        );

    }
    catch (error) {

        console.log("Application error:");

        console.log(error);


        response.send(
            "Something went wrong while applying."
        );

    }

});


// ==============================
// MY APPLICATIONS
// ==============================

app.get("/applications", async (request, response) => {

    if (!request.session.userId) {

        return response.redirect("/login");

    }


    // Only students can view applications

    if (request.session.role !== "Student") {

        return response.send(
            "Only students can view applications."
        );

    }


    const userId = request.session.userId;


    try {

        const result = await sql.query`

            SELECT

                Applications.ApplicationId,

                Applications.ApplicationDate,

                Applications.Status,

                Jobs.JobId,

                Jobs.Title,

                Jobs.Company,

                Jobs.Location,

                Jobs.JobType

            FROM Applications

            INNER JOIN Jobs

                ON Applications.JobId = Jobs.JobId

            WHERE Applications.UserId = ${userId}

            ORDER BY Applications.ApplicationDate DESC

        `;


        console.log("Applications fetched:");

        console.log(result.recordset);


        response.render("applications", {

            applications: result.recordset

        });

    }
    catch (error) {

        console.log("Applications error:");

        console.log(error);


        response.send(
            "Something went wrong while loading applications."
        );

    }

});


// ==============================
// LOGIN PAGE - GET
// ==============================

app.get("/login", (request, response) => {

    response.render("login");

});


// ==============================
// LOGIN FORM - POST
// ==============================

app.post("/login", async (request, response) => {

    const {
        email,
        password
    } = request.body;


    // Check fields

    if (!email || !password) {

        return response.send(
            "Please enter email and password."
        );

    }


    try {

        // Check user in database

        const result = await sql.query`

            SELECT *

            FROM Users

            WHERE Email = ${email}

            AND Password = ${password}

        `;


        // If user exists

        if (result.recordset.length > 0) {

            const user = result.recordset[0];


            // Store user information in session

            request.session.userId = user.UserId;

            request.session.fullName = user.FullName;

            request.session.email = user.Email;

            request.session.role = user.Role;


            console.log("Login successful!");

            console.log(
                "User:",
                user.FullName
            );

            console.log(
                "Role:",
                user.Role
            );


            // Go to dashboard

            return response.redirect("/dashboard");

        }


        // Invalid login

        response.send(
            "Invalid email or password."
        );

    }
    catch (error) {

        console.log("Login error:");

        console.log(error);


        response.send(
            "Something went wrong during login."
        );

    }

});


// ==============================
// DASHBOARD
// ==============================

app.get("/dashboard", async (request, response) => {

    // Check whether user is logged in

    if (!request.session.userId) {

        return response.redirect("/login");

    }


    // Get user information from session

    const user = {

        userId: request.session.userId,

        fullName: request.session.fullName,

        email: request.session.email,

        role: request.session.role

    };


    // ==============================
    // COMPANY DASHBOARD
    // ==============================

    if (user.role === "Company") {

        try {

            const result = await sql.query`

                SELECT *

                FROM Companies

                WHERE UserId = ${user.userId}

            `;


            if (result.recordset.length === 0) {

                return response.send(
                    "Company profile not found."
                );

            }


            const company = result.recordset[0];


            return response.render(
                "company-dashboard",
                {
                    user: user,
                    company: company
                }
            );

        }
        catch (error) {

            console.log(
                "Company dashboard error:"
            );

            console.log(error);


            return response.send(
                "Something went wrong while loading company dashboard."
            );

        }

    }


    // ==============================
    // STUDENT DASHBOARD
    // ==============================

    response.render("dashboard", {

        user: user

    });

});


// ==============================
// STUDENT REGISTER PAGE - GET
// ==============================

app.get("/register", (request, response) => {

    response.render("register");

});


// ==============================
// STUDENT REGISTER FORM - POST
// ==============================

app.post("/register", async (request, response) => {

    const {
        fullName,
        email,
        password,
        confirmPassword
    } = request.body;


    // Check all fields

    if (
        !fullName ||
        !email ||
        !password ||
        !confirmPassword
    ) {

        return response.send(
            "Please fill all fields."
        );

    }


    // Check passwords

    if (password !== confirmPassword) {

        return response.send(
            "Passwords do not match."
        );

    }


    try {

        // Check email already exists

        const checkUser = await sql.query`

            SELECT *

            FROM Users

            WHERE Email = ${email}

        `;


        if (checkUser.recordset.length > 0) {

            return response.send(
                "Email already registered."
            );

        }


        // Insert student user

        await sql.query`

            INSERT INTO Users
            (
                FullName,
                Email,
                Password,
                Role
            )

            VALUES
            (
                ${fullName},
                ${email},
                ${password},
                'Student'
            )

        `;


        console.log(
            "Registration successful!"
        );

        console.log(
            "Name:",
            fullName
        );

        console.log(
            "Email:",
            email
        );


        response.send(
            "Registration successful!"
        );

    }
    catch (error) {

        console.log(
            "Registration error:"
        );

        console.log(error);


        response.send(
            "Something went wrong during registration."
        );

    }

});


// ==============================
// COMPANY REGISTER PAGE - GET
// ==============================

app.get("/company-register", (request, response) => {

    response.render("company-register");

});


// ==============================
// COMPANY REGISTER FORM - POST
// ==============================

app.post("/company-register", async (request, response) => {

    const {
        companyName,
        email,
        password,
        confirmPassword,
        phone,
        location,
        website,
        description
    } = request.body;


    // Check required fields

    if (
        !companyName ||
        !email ||
        !password ||
        !confirmPassword
    ) {

        return response.send(
            "Please fill all required fields."
        );

    }


    // Check passwords

    if (password !== confirmPassword) {

        return response.send(
            "Passwords do not match."
        );

    }


    try {

        // Check whether email already exists

        const checkUser = await sql.query`

            SELECT *

            FROM Users

            WHERE Email = ${email}

        `;


        if (checkUser.recordset.length > 0) {

            return response.send(
                "Email already registered."
            );

        }


        // Create company user account

        const userResult = await sql.query`

            INSERT INTO Users
            (
                FullName,
                Email,
                Password,
                Role
            )

            OUTPUT INSERTED.UserId

            VALUES
            (
                ${companyName},
                ${email},
                ${password},
                'Company'
            )

        `;


        const userId =
            userResult.recordset[0].UserId;


        // Create company profile

        await sql.query`

            INSERT INTO Companies
            (
                UserId,
                CompanyName,
                Email,
                Phone,
                Location,
                Description,
                Website
            )

            VALUES
            (
                ${userId},
                ${companyName},
                ${email},
                ${phone},
                ${location},
                ${description},
                ${website}
            )

        `;


        console.log(
            "Company registration successful!"
        );

        console.log(
            "Company:",
            companyName
        );

        console.log(
            "Email:",
            email
        );


        response.send(
            "Company registration successful! You can now login."
        );

    }
    catch (error) {

        console.log(
            "Company registration error:"
        );

        console.log(error);


        response.send(
            "Something went wrong during company registration."
        );

    }

});


// ==============================
// COMPANY POST JOB PAGE - GET
// ==============================

app.get("/company/post-job", async (request, response) => {

    // Check login

    if (!request.session.userId) {

        return response.redirect("/login");

    }


    // Only company can access

    if (request.session.role !== "Company") {

        return response.send(
            "Access denied. Only companies can post jobs."
        );

    }


    try {

        // Find company

        const result = await sql.query`

            SELECT *

            FROM Companies

            WHERE UserId = ${request.session.userId}

        `;


        if (result.recordset.length === 0) {

            return response.send(
                "Company profile not found."
            );

        }


        const company = result.recordset[0];


        // Open post job page

        response.render("post-job", {

            company: company

        });

    }
    catch (error) {

        console.log(
            "Post job page error:"
        );

        console.log(error);


        response.send(
            "Something went wrong while loading the post job page."
        );

    }

});


// ==============================
// COMPANY POST JOB FORM - POST
// ==============================

app.post("/company/post-job", async (request, response) => {

    const {
        title,
        jobType,
        location,
        skills,
        salary,
        description
    } = request.body;


    // Check login

    if (!request.session.userId) {

        return response.redirect("/login");

    }


    // Only company can post

    if (request.session.role !== "Company") {

        return response.send(
            "Access denied. Only companies can post jobs."
        );

    }


    // Check required fields

    if (
        !title ||
        !jobType ||
        !location ||
        !skills ||
        !description
    ) {

        return response.send(
            "Please fill all required fields."
        );

    }


    try {

        // Find company

        const companyResult = await sql.query`

            SELECT *

            FROM Companies

            WHERE UserId = ${request.session.userId}

        `;


        if (companyResult.recordset.length === 0) {

            return response.send(
                "Company profile not found."
            );

        }


        const company = companyResult.recordset[0];


        // Insert job

        await sql.query`

            INSERT INTO Jobs
            (
                Title,
                Company,
                Location,
                JobType,
                Description,
                Skills,
                Salary,
                CompanyId
            )

            VALUES
            (
                ${title},
                ${company.CompanyName},
                ${location},
                ${jobType},
                ${description},
                ${skills},
                ${salary},
                ${company.CompanyId}
            )

        `;


        console.log(
            "Job posted successfully!"
        );

        console.log(
            "Company:",
            company.CompanyName
        );

        console.log(
            "Job:",
            title
        );


        // Go to My Posted Jobs

        response.redirect("/company/jobs");

    }
    catch (error) {

        console.log(
            "Post job error:"
        );

        console.log(error);


        response.send(
            "Something went wrong while posting the job."
        );

    }

});


// ==============================
// COMPANY MY POSTED JOBS
// ==============================

app.get("/company/jobs", async (request, response) => {

    // Check login

    if (!request.session.userId) {

        return response.redirect("/login");

    }


    // Only company can access

    if (request.session.role !== "Company") {

        return response.send(
            "Access denied. Only companies can view their posted jobs."
        );

    }


    try {

        // Find company

        const companyResult = await sql.query`

            SELECT
                CompanyId,
                CompanyName

            FROM Companies

            WHERE UserId = ${request.session.userId}

        `;


        if (companyResult.recordset.length === 0) {

            return response.send(
                "Company profile not found."
            );

        }


        const company = companyResult.recordset[0];


        // Find jobs posted by this company

        const jobResult = await sql.query`

            SELECT *

            FROM Jobs

            WHERE CompanyId = ${company.CompanyId}

            ORDER BY PostedDate DESC

        `;


        console.log(
            "Company jobs fetched:"
        );

        console.log(
            jobResult.recordset
        );


        // Open My Posted Jobs page

        response.render(
            "my-posted-jobs",
            {
                company: company,
                jobs: jobResult.recordset
            }
        );

    }
    catch (error) {

        console.log(
            "My posted jobs error:"
        );

        console.log(error);


        response.send(
            "Something went wrong while loading your posted jobs."
        );

    }

});


// ==============================
// COMPANY VIEW APPLICANTS
// ==============================

app.get("/company/applicants/:jobId", async (request, response) => {

    // Check login

    if (!request.session.userId) {

        return response.redirect("/login");

    }


    // Only company can access

    if (request.session.role !== "Company") {

        return response.send(
            "Access denied. Only companies can view applicants."
        );

    }


    const jobId = request.params.jobId;


    try {

        // Find company

        const companyResult = await sql.query`

            SELECT
                CompanyId,
                CompanyName

            FROM Companies

            WHERE UserId = ${request.session.userId}

        `;


        if (companyResult.recordset.length === 0) {

            return response.send(
                "Company profile not found."
            );

        }


        const company = companyResult.recordset[0];


        // Check whether this job belongs to this company

        const jobResult = await sql.query`

            SELECT *

            FROM Jobs

            WHERE JobId = ${jobId}

            AND CompanyId = ${company.CompanyId}

        `;


        if (jobResult.recordset.length === 0) {

            return response.send(
                "Job not found or this job does not belong to your company."
            );

        }


        const job = jobResult.recordset[0];


        // Get applicants

        const applicantResult = await sql.query`

            SELECT

                Applications.ApplicationId,

                Applications.ApplicationDate,

                Applications.Status,

                Users.UserId,

                Users.FullName,

                Users.Email

            FROM Applications

            INNER JOIN Users

                ON Applications.UserId = Users.UserId

            WHERE Applications.JobId = ${jobId}

            ORDER BY Applications.ApplicationDate DESC

        `;


        console.log(
            "Applicants fetched:"
        );

        console.log(
            applicantResult.recordset
        );


        // Open applicants page

        response.render(
            "applicants",
            {
                company: company,
                job: job,
                applicants: applicantResult.recordset
            }
        );

    }
    catch (error) {

        console.log(
            "Applicants error:"
        );

        console.log(error);


        response.send(
            "Something went wrong while loading applicants."
        );

    }

});


// ==============================
// COMPANY ACCEPT / REJECT APPLICANT
// ==============================

app.post("/company/applicants/:applicationId/status", async (request, response) => {

    // Check login

    if (!request.session.userId) {

        return response.redirect("/login");

    }


    // Only company can update applicant status

    if (request.session.role !== "Company") {

        return response.send(
            "Access denied. Only companies can update applicant status."
        );

    }


    const applicationId =
        request.params.applicationId;

    const status =
        request.body.status;


    // Allow only Accepted or Rejected

    if (
        status !== "Accepted" &&
        status !== "Rejected"
    ) {

        return response.send(
            "Invalid application status."
        );

    }


    try {

        // ==============================
        // FIND COMPANY
        // ==============================

        const companyResult = await sql.query`

            SELECT
                CompanyId

            FROM Companies

            WHERE UserId = ${request.session.userId}

        `;


        if (companyResult.recordset.length === 0) {

            return response.send(
                "Company profile not found."
            );

        }


        const companyId =
            companyResult.recordset[0].CompanyId;


        // ==============================
        // CHECK APPLICATION
        // ==============================

        // Make sure this application
        // belongs to a job posted by
        // the logged-in company

        const applicationResult = await sql.query`

            SELECT

                Applications.ApplicationId,

                Applications.JobId

            FROM Applications

            INNER JOIN Jobs

                ON Applications.JobId = Jobs.JobId

            WHERE Applications.ApplicationId = ${applicationId}

            AND Jobs.CompanyId = ${companyId}

        `;


        if (applicationResult.recordset.length === 0) {

            return response.send(
                "Application not found or access denied."
            );

        }


        const jobId =
            applicationResult.recordset[0].JobId;


        // ==============================
        // UPDATE STATUS
        // ==============================

        await sql.query`

            UPDATE Applications

            SET Status = ${status}

            WHERE ApplicationId = ${applicationId}

        `;


        console.log(
            "Applicant status updated!"
        );

        console.log(
            "Application ID:",
            applicationId
        );

        console.log(
            "New Status:",
            status
        );


        // ==============================
        // RETURN TO APPLICANTS PAGE
        // ==============================

        response.redirect(
            "/company/applicants/" + jobId
        );

    }
    catch (error) {

        console.log(
            "Update applicant status error:"
        );

        console.log(error);


        response.send(
            "Something went wrong while updating applicant status."
        );

    }

});


// ==============================
// LOGOUT
// ==============================

app.get("/logout", (request, response) => {

    request.session.destroy((error) => {

        if (error) {

            console.log(
                "Logout error:"
            );

            console.log(error);


            return response.send(
                "Unable to logout."
            );

        }


        console.log(
            "User logged out successfully!"
        );


        response.redirect("/login");

    });

});


// ==============================
// START SERVER
// ==============================

app.listen(3000, () => {

    console.log(
        "CareerConnect server is running on port 3000"
    );

});