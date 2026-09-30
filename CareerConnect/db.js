const sql = require("mssql/msnodesqlv8");

const config = {
    server: "DESKTOP-LDBJA37\\SAMISHA",
    database: "CareerConnectDB",

    options: {
        trustedConnection: true,
        trustServerCertificate: true
    }
};

async function connectDatabase() {

    try {

        await sql.connect(config);

        console.log("SQL Server connected successfully!");

    }
    catch (error) {

        console.log("Database connection failed:");
        console.log(error);

    }

}

connectDatabase();

module.exports = sql;