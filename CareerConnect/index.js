const express = require("express");
const sql = require("./db");
const session = require("express-session");
const multer = require("multer");
const path = require("path");

const app = express();


// ==========================================
// MULTER - RESUME UPLOAD
// ==========================================

const storage = multer.diskStorage({

    destination: function (request, file, callback) {

        callback(
            null,
            "uploads/resumes/"
        );

    },

    filename: function (request, file, callback) {

        const uniqueName =
            Date.now() +
            "-" +
            file.originalname;

        callback(
            null,
            uniqueName
        );

    }

});


const upload = multer({

    storage: storage,

    fileFilter: function (request, file, callback) {

        const extension =
            path.extname(file.originalname).toLowerCase();

        if (extension === ".pdf") {

            callback(null, true);

        }
        else {

            callback(
                new Error("Only PDF files are allowed.")
            );

        }

    },

    limits: {

        fileSize: 5 * 1024 * 1024

    }

});


// ==========================================
// EXPRESS SETTINGS
// ==========================================

app.set("view engine", "ejs");

app.use(express.static("public"));

app.use(
    "/uploads",
    express.static("uploads")
);

app.use(
    express.urlencoded({
        extended: true
    })
);


// ==========================================
// SESSION
// ==========================================

app.use(
    session({

        secret: "careerconnect-secret-key",

        resave: false,

        saveUninitialized: false

    })
);


// ==========================================
// HOME
// ==========================================

app.get("/", (request, response) => {

    const student = {

        name: "Samiksha",

        course: "B.Tech Computer Science",

        city: "Sangli"

    };

    response.render(
        "home",
        {
            student: student
        }
    );

});


// ==========================================
// JOBS
// ==========================================

app.get("/jobs", async (request, response) => {

    try {

        const result = await sql.query`

            SELECT *

            FROM Jobs

            ORDER BY PostedDate DESC

        `;

        console.log(
            "Jobs fetched:"
        );

        console.log(
            result.recordset
        );


        response.render(
            "jobs",
            {
                jobs: result.recordset
            }
        );

    }

    catch (error) {

        console.log(
            "Jobs error:"
        );

        console.log(error);


        response.send(
            "Something went wrong while loading jobs."
        );

    }

});


// ==========================================
// APPLY FOR JOB
// ==========================================

app.post("/apply/:jobId", async (request, response) => {

    if (!request.session.userId) {

        return response.redirect("/login");

    }


    if (request.session.role !== "Student") {

        return response.send(
            "Only students can apply for jobs."
        );

    }


    const userId =
        request.session.userId;

    const jobId =
        request.params.jobId;


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


        // Check duplicate application

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


        console.log(
            "Application submitted!"
        );

        console.log(
            "User ID:",
            userId
        );

        console.log(
            "Job ID:",
            jobId
        );


        response.send(
            "Application submitted successfully!"
        );

    }

    catch (error) {

        console.log(
            "Application error:"
        );

        console.log(error);


        response.send(
            "Something went wrong while applying."
        );

    }

});


// ==========================================
// STUDENT APPLICATIONS
// ==========================================

app.get("/applications", async (request, response) => {

    if (!request.session.userId) {

        return response.redirect("/login");

    }


    if (request.session.role !== "Student") {

        return response.send(
            "Only students can view applications."
        );

    }


    const userId =
        request.session.userId;


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


        console.log(
            "Applications fetched:"
        );

        console.log(
            result.recordset
        );


        response.render(
            "applications",
            {
                applications:
                    result.recordset
            }
        );

    }

    catch (error) {

        console.log(
            "Applications error:"
        );

        console.log(error);


        response.send(
            "Something went wrong while loading applications."
        );

    }

});


// ==========================================
// LOGIN PAGE
// ==========================================

app.get("/login", (request, response) => {

    response.render("login");

});


// ==========================================
// LOGIN
// ==========================================

app.post("/login", async (request, response) => {

    const {

        email,

        password

    } = request.body;


    if (!email || !password) {

        return response.send(
            "Please enter email and password."
        );

    }


    try {

        const result = await sql.query`

            SELECT *

            FROM Users

            WHERE Email = ${email}

            AND Password = ${password}

        `;


        if (result.recordset.length > 0) {

            const user =
                result.recordset[0];


            request.session.userId =
                user.UserId;

            request.session.fullName =
                user.FullName;

            request.session.email =
                user.Email;

            request.session.role =
                user.Role;


            console.log(
                "Login successful!"
            );

            console.log(
                "User:",
                user.FullName
            );

            console.log(
                "Role:",
                user.Role
            );


            return response.redirect(
                "/dashboard"
            );

        }


        response.send(
            "Invalid email or password."
        );

    }

    catch (error) {

        console.log(
            "Login error:"
        );

        console.log(error);


        response.send(
            "Something went wrong during login."
        );

    }

});


// ==========================================
// DASHBOARD
// ==========================================

app.get("/dashboard", async (request, response) => {

    if (!request.session.userId) {

        return response.redirect("/login");

    }


    const user = {

        userId:
            request.session.userId,

        fullName:
            request.session.fullName,

        email:
            request.session.email,

        role:
            request.session.role

    };


    // ======================================
    // ADMIN DASHBOARD
    // ======================================

    if (user.role === "Admin") {

        return response.render(
            "admin-dashboard",
            {
                user: user
            }
        );

    }


    // ======================================
    // COMPANY DASHBOARD
    // ======================================

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


            const company =
                result.recordset[0];


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


    // ======================================
    // STUDENT DASHBOARD
    // ======================================

    response.render(
        "dashboard",
        {
            user: user
        }
    );

});


// ==========================================
// STUDENT REGISTER PAGE
// ==========================================

app.get("/register", (request, response) => {

    response.render("register");

});


// ==========================================
// STUDENT REGISTER
// ==========================================

app.post("/register", async (request, response) => {

    const {

        fullName,

        email,

        password,

        confirmPassword

    } = request.body;


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


    if (password !== confirmPassword) {

        return response.send(
            "Passwords do not match."
        );

    }


    try {

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


// ==========================================
// COMPANY REGISTER PAGE
// ==========================================

app.get("/company-register", (request, response) => {

    response.render("company-register");

});


// ==========================================
// COMPANY REGISTER
// ==========================================

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


    if (password !== confirmPassword) {

        return response.send(
            "Passwords do not match."
        );

    }


    try {

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


// ==========================================
// POST JOB PAGE
// ==========================================

app.get("/company/post-job", async (request, response) => {

    if (!request.session.userId) {

        return response.redirect("/login");

    }


    if (request.session.role !== "Company") {

        return response.send(
            "Access denied. Only companies can post jobs."
        );

    }


    try {

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


        const company =
            result.recordset[0];


        response.render(
            "post-job",
            {
                company: company
            }
        );

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


// ==========================================
// POST JOB
// ==========================================

app.post("/company/post-job", async (request, response) => {

    const {

        title,

        jobType,

        location,

        skills,

        salary,

        description

    } = request.body;


    if (!request.session.userId) {

        return response.redirect("/login");

    }


    if (request.session.role !== "Company") {

        return response.send(
            "Access denied. Only companies can post jobs."
        );

    }


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


        const company =
            companyResult.recordset[0];


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


        response.redirect(
            "/company/jobs"
        );

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


// ==========================================
// COMPANY POSTED JOBS
// ==========================================

app.get("/company/jobs", async (request, response) => {

    if (!request.session.userId) {

        return response.redirect("/login");

    }


    if (request.session.role !== "Company") {

        return response.send(
            "Access denied. Only companies can view their posted jobs."
        );

    }


    try {

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


        const company =
            companyResult.recordset[0];


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


// ==========================================
// VIEW APPLICANTS
// ==========================================

app.get("/company/applicants/:jobId", async (request, response) => {

    if (!request.session.userId) {

        return response.redirect("/login");

    }


    if (request.session.role !== "Company") {

        return response.send(
            "Access denied. Only companies can view applicants."
        );

    }


    const jobId =
        request.params.jobId;


    try {

        // ======================================
        // GET COMPANY
        // ======================================

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


        const company =
            companyResult.recordset[0];


        // ======================================
        // CHECK JOB BELONGS TO COMPANY
        // ======================================

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


        const job =
            jobResult.recordset[0];


        // ======================================
        // GET APPLICANTS + STUDENT PROFILE
        // ======================================

        const applicantResult = await sql.query`

            SELECT

                Applications.ApplicationId,

                Applications.ApplicationDate,

                Applications.Status,

                Users.UserId,

                Users.FullName,

                Users.Email,

                StudentProfiles.Phone,

                StudentProfiles.College,

                StudentProfiles.Course,

                StudentProfiles.Education,

                StudentProfiles.Skills,

                StudentProfiles.ResumePath

            FROM Applications

            INNER JOIN Users

                ON Applications.UserId = Users.UserId

            LEFT JOIN StudentProfiles

                ON Applications.UserId = StudentProfiles.UserId

            WHERE Applications.JobId = ${jobId}

            ORDER BY Applications.ApplicationDate DESC

        `;


        console.log(
            "Applicants fetched:"
        );

        console.log(
            applicantResult.recordset
        );


        response.render(
            "applicants",
            {

                company: company,

                job: job,

                applicants:
                    applicantResult.recordset

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


// ==========================================
// ACCEPT / REJECT APPLICANT
// ==========================================

app.post(
    "/company/applicants/:applicationId/status",

    async (request, response) => {

        if (!request.session.userId) {

            return response.redirect("/login");

        }


        if (request.session.role !== "Company") {

            return response.send(
                "Access denied. Only companies can update applicant status."
            );

        }


        const applicationId =
            request.params.applicationId;


        const status =
            request.body.status;


        if (

            status !== "Accepted" &&

            status !== "Rejected"

        ) {

            return response.send(
                "Invalid application status."
            );

        }


        try {

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


            response.redirect(
                "/company/applicants/" +
                jobId
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

    }

);


// ==========================================
// STUDENT PROFILE
// ==========================================

app.get("/profile", async (request, response) => {

    if (!request.session.userId) {

        return response.redirect("/login");

    }


    if (request.session.role !== "Student") {

        return response.send(
            "Access denied. Only students can view their profile."
        );

    }


    try {

        const userResult = await sql.query`

            SELECT

                UserId,

                FullName,

                Email

            FROM Users

            WHERE UserId = ${request.session.userId}

        `;


        if (userResult.recordset.length === 0) {

            return response.send(
                "Student not found."
            );

        }


        const user =
            userResult.recordset[0];


        const profileResult = await sql.query`

            SELECT *

            FROM StudentProfiles

            WHERE UserId = ${request.session.userId}

        `;


        const profile =

            profileResult.recordset.length > 0

                ? profileResult.recordset[0]

                : null;


        response.render(
            "profile",
            {
                user: user,
                profile: profile
            }
        );

    }

    catch (error) {

        console.log(
            "Profile page error:"
        );

        console.log(error);


        response.send(
            "Something went wrong while loading your profile."
        );

    }

});


// ==========================================
// SAVE STUDENT PROFILE + RESUME
// ==========================================

app.post(
    "/profile",

    upload.single("resume"),

    async (request, response) => {

        if (!request.session.userId) {

            return response.redirect("/login");

        }


        if (request.session.role !== "Student") {

            return response.send(
                "Access denied. Only students can update their profile."
            );

        }


        const {

            fullName,

            phone,

            college,

            course,

            education,

            skills

        } = request.body;


        if (!fullName) {

            return response.send(
                "Full Name is required."
            );

        }


        try {

            // ======================================
            // UPDATE USER NAME
            // ======================================

            await sql.query`

                UPDATE Users

                SET FullName = ${fullName}

                WHERE UserId = ${request.session.userId}

            `;


            // ======================================
            // GET EXISTING PROFILE
            // ======================================

            const profileResult = await sql.query`

                SELECT

                    ProfileId,

                    ResumePath

                FROM StudentProfiles

                WHERE UserId = ${request.session.userId}

            `;


            let resumePath = null;


            // ======================================
            // NEW RESUME
            // ======================================

            if (request.file) {

                resumePath =
                    "/uploads/resumes/" +
                    request.file.filename;

            }


            // ======================================
            // UPDATE EXISTING PROFILE
            // ======================================

            if (profileResult.recordset.length > 0) {

                // Keep old resume if no new resume
                // was uploaded

                if (!resumePath) {

                    resumePath =
                        profileResult.recordset[0].ResumePath;

                }


                await sql.query`

                    UPDATE StudentProfiles

                    SET

                        Phone = ${phone},

                        College = ${college},

                        Course = ${course},

                        Education = ${education},

                        Skills = ${skills},

                        ResumePath = ${resumePath}

                    WHERE UserId = ${request.session.userId}

                `;

            }


            // ======================================
            // CREATE NEW PROFILE
            // ======================================

            else {

                await sql.query`

                    INSERT INTO StudentProfiles
                    (
                        UserId,
                        Phone,
                        College,
                        Course,
                        Education,
                        Skills,
                        ResumePath
                    )

                    VALUES
                    (
                        ${request.session.userId},

                        ${phone},

                        ${college},

                        ${course},

                        ${education},

                        ${skills},

                        ${resumePath}

                    )

                `;

            }


            // ======================================
            // UPDATE SESSION NAME
            // ======================================

            request.session.fullName =
                fullName;


            console.log(
                "Student profile saved successfully!"
            );


            if (request.file) {

                console.log(
                    "Resume uploaded:",
                    request.file.filename
                );

            }


            response.redirect(
                "/profile"
            );

        }

        catch (error) {

            console.log(
                "Save profile error:"
            );

            console.log(error);


            response.send(
                "Something went wrong while saving your profile."
            );

        }

    }

);


// ==========================================
// LOGOUT
// ==========================================

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


        response.redirect(
            "/login"
        );

    });

});


// ==========================================
// START SERVER
// ==========================================

app.listen(3000, () => {

    console.log(
        "CareerConnect server is running on port 3000"
    );

});