namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnnualLeaves",
                c => new
                    {
                        annualLeaveID = c.Int(nullable: false, identity: true),
                        startDate = c.DateTime(nullable: false),
                        finishDate = c.DateTime(nullable: false),
                        doctorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.annualLeaveID)
                .ForeignKey("dbo.Doctors", t => t.doctorID, cascadeDelete: true)
                .Index(t => t.doctorID);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        doctorID = c.Int(nullable: false, identity: true),
                        doctorPreName = c.String(maxLength: 20),
                        doctorLastName = c.String(maxLength: 20),
                        birthDate = c.DateTime(nullable: false),
                        age = c.Int(nullable: false),
                        salary = c.Int(nullable: false),
                        socSecNo = c.Int(nullable: false),
                        doctorPhoneNo = c.Int(nullable: false),
                        departmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.doctorID)
                .ForeignKey("dbo.Departments", t => t.departmentID, cascadeDelete: true)
                .Index(t => t.departmentID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        departmentID = c.Int(nullable: false, identity: true),
                        departmentName = c.String(maxLength: 100),
                        locationAdress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.departmentID);
            
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        appointmentID = c.Int(nullable: false, identity: true),
                        appDate = c.DateTime(nullable: false),
                        appFee = c.Int(nullable: false),
                        doctorID = c.Int(nullable: false),
                        patientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.appointmentID)
                .ForeignKey("dbo.Doctors", t => t.doctorID, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.patientID, cascadeDelete: true)
                .Index(t => t.doctorID)
                .Index(t => t.patientID);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        patientID = c.Int(nullable: false, identity: true),
                        patientPreName = c.String(maxLength: 20),
                        patientLastName = c.String(maxLength: 20),
                        patientBirthDate = c.DateTime(nullable: false),
                        patientAge = c.Int(nullable: false),
                        patientSocSecNo = c.Int(nullable: false),
                        userID = c.Int(),
                    })
                .PrimaryKey(t => t.patientID)
                .ForeignKey("dbo.Users", t => t.userID)
                .Index(t => t.userID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userID = c.Int(nullable: false, identity: true),
                        userPreName = c.String(maxLength: 20),
                        userLastName = c.String(maxLength: 20),
                        userEmail = c.String(maxLength: 50),
                        userPassword = c.String(maxLength: 20),
                        userType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.userID);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        billID = c.Int(nullable: false, identity: true),
                        fee = c.Int(nullable: false),
                        appointmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.billID)
                .ForeignKey("dbo.Appointments", t => t.appointmentID, cascadeDelete: true)
                .Index(t => t.appointmentID);
            
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        medicineID = c.Int(nullable: false, identity: true),
                        medicineName = c.String(maxLength: 20),
                        prescriptionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.medicineID)
                .ForeignKey("dbo.Prescriptions", t => t.prescriptionID, cascadeDelete: true)
                .Index(t => t.prescriptionID);
            
            CreateTable(
                "dbo.Prescriptions",
                c => new
                    {
                        prescriptionID = c.Int(nullable: false, identity: true),
                        useIntr = c.String(maxLength: 200),
                        duration = c.Int(nullable: false),
                        appointmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.prescriptionID)
                .ForeignKey("dbo.Appointments", t => t.appointmentID, cascadeDelete: true)
                .Index(t => t.appointmentID);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        roomID = c.Int(nullable: false, identity: true),
                        roomAvailability = c.Boolean(nullable: false),
                        patientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.roomID)
                .ForeignKey("dbo.Patients", t => t.patientID, cascadeDelete: true)
                .Index(t => t.patientID);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        staffID = c.Int(nullable: false, identity: true),
                        staffPreName = c.String(maxLength: 20),
                        staffLastName = c.String(maxLength: 20),
                        staffSalary = c.Int(nullable: false),
                        staffTask = c.String(maxLength: 50),
                        stafSocSecNo = c.Int(nullable: false),
                        roomID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.staffID)
                .ForeignKey("dbo.Rooms", t => t.roomID, cascadeDelete: true)
                .Index(t => t.roomID);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        stockID = c.Int(nullable: false, identity: true),
                        stockName = c.String(maxLength: 50),
                        stockAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.stockID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staffs", "roomID", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "patientID", "dbo.Patients");
            DropForeignKey("dbo.Medicines", "prescriptionID", "dbo.Prescriptions");
            DropForeignKey("dbo.Prescriptions", "appointmentID", "dbo.Appointments");
            DropForeignKey("dbo.Bills", "appointmentID", "dbo.Appointments");
            DropForeignKey("dbo.Appointments", "patientID", "dbo.Patients");
            DropForeignKey("dbo.Patients", "userID", "dbo.Users");
            DropForeignKey("dbo.Appointments", "doctorID", "dbo.Doctors");
            DropForeignKey("dbo.AnnualLeaves", "doctorID", "dbo.Doctors");
            DropForeignKey("dbo.Doctors", "departmentID", "dbo.Departments");
            DropIndex("dbo.Staffs", new[] { "roomID" });
            DropIndex("dbo.Rooms", new[] { "patientID" });
            DropIndex("dbo.Prescriptions", new[] { "appointmentID" });
            DropIndex("dbo.Medicines", new[] { "prescriptionID" });
            DropIndex("dbo.Bills", new[] { "appointmentID" });
            DropIndex("dbo.Patients", new[] { "userID" });
            DropIndex("dbo.Appointments", new[] { "patientID" });
            DropIndex("dbo.Appointments", new[] { "doctorID" });
            DropIndex("dbo.Doctors", new[] { "departmentID" });
            DropIndex("dbo.AnnualLeaves", new[] { "doctorID" });
            DropTable("dbo.Stocks");
            DropTable("dbo.Staffs");
            DropTable("dbo.Rooms");
            DropTable("dbo.Prescriptions");
            DropTable("dbo.Medicines");
            DropTable("dbo.Bills");
            DropTable("dbo.Users");
            DropTable("dbo.Patients");
            DropTable("dbo.Appointments");
            DropTable("dbo.Departments");
            DropTable("dbo.Doctors");
            DropTable("dbo.AnnualLeaves");
        }
    }
}
