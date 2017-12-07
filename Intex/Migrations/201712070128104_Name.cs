namespace Intex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Name : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appearance",
                c => new
                    {
                        AppearanceID = c.Int(nullable: false, identity: true),
                        AppearanceDesc = c.String(),
                    })
                .PrimaryKey(t => t.AppearanceID);
            
            CreateTable(
                "dbo.Assay",
                c => new
                    {
                        AssayID = c.Int(nullable: false, identity: true),
                        AssayName = c.String(),
                        Desc = c.String(),
                    })
                .PrimaryKey(t => t.AssayID);
            
            CreateTable(
                "dbo.AssayTest",
                c => new
                    {
                        TestTypeID = c.Int(nullable: false),
                        AssayID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TestTypeID, t.AssayID })
                .ForeignKey("dbo.Assay", t => t.AssayID, cascadeDelete: true)
                .ForeignKey("dbo.TestType", t => t.TestTypeID, cascadeDelete: true)
                .Index(t => t.TestTypeID)
                .Index(t => t.AssayID);
            
            CreateTable(
                "dbo.TestType",
                c => new
                    {
                        TestTypeID = c.Int(nullable: false, identity: true),
                        TestName = c.String(),
                        Desc = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TestTypeID);
            
            CreateTable(
                "dbo.Compound",
                c => new
                    {
                        CompoundID = c.Int(nullable: false, identity: true),
                        CustomerID = c.String(maxLength: 128),
                        CompoundName = c.String(),
                        MolecularMass = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CompoundID)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerID = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        StateID = c.Int(nullable: false),
                        CountryID = c.Int(nullable: false),
                        PostalCode = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID)
                .ForeignKey("dbo.Country", t => t.CountryID, cascadeDelete: true)
                .ForeignKey("dbo.State", t => t.StateID, cascadeDelete: true)
                .Index(t => t.StateID)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        CountryID = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                    })
                .PrimaryKey(t => t.CountryID);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        StateID = c.Int(nullable: false, identity: true),
                        StateAbbrev = c.String(),
                        StateName = c.String(),
                    })
                .PrimaryKey(t => t.StateID);
            
            CreateTable(
                "dbo.EmployeeRole",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        RoleDesc = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        WageTypeID = c.Int(nullable: false),
                        Address = c.String(),
                        City = c.String(),
                        StateID = c.Int(nullable: false),
                        CountryID = c.Int(nullable: false),
                        PostalCode = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        RoleID = c.Int(nullable: false),
                        LocationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.EmployeeRole", t => t.RoleID, cascadeDelete: true)
                .ForeignKey("dbo.Location", t => t.LocationID, cascadeDelete: true)
                .ForeignKey("dbo.WageType", t => t.WageTypeID, cascadeDelete: true)
                .Index(t => t.WageTypeID)
                .Index(t => t.RoleID)
                .Index(t => t.LocationID);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        LocationName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        StateID = c.Int(nullable: false),
                        CountryID = c.Int(nullable: false),
                        PostalCode = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.LocationID)
                .ForeignKey("dbo.Country", t => t.CountryID, cascadeDelete: true)
                .ForeignKey("dbo.State", t => t.StateID, cascadeDelete: true)
                .Index(t => t.StateID)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.WageType",
                c => new
                    {
                        WageTypeID = c.Int(nullable: false, identity: true),
                        WageTypeDesc = c.String(),
                    })
                .PrimaryKey(t => t.WageTypeID);
            
            CreateTable(
                "dbo.OrderCompound",
                c => new
                    {
                        CompoundID = c.Int(nullable: false),
                        OrderNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CompoundID, t.OrderNumber })
                .ForeignKey("dbo.Compound", t => t.CompoundID, cascadeDelete: true)
                .ForeignKey("dbo.WorkOrder", t => t.OrderNumber, cascadeDelete: true)
                .Index(t => t.CompoundID)
                .Index(t => t.OrderNumber);
            
            CreateTable(
                "dbo.WorkOrder",
                c => new
                    {
                        OrderNumber = c.Int(nullable: false, identity: true),
                        OrderTypeID = c.Int(nullable: false),
                        CustomerID = c.String(maxLength: 128),
                        QuoteDate = c.DateTime(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        StatusID = c.Int(nullable: false),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.OrderNumber)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .ForeignKey("dbo.OrderStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("dbo.OrderType", t => t.OrderTypeID, cascadeDelete: true)
                .Index(t => t.OrderTypeID)
                .Index(t => t.CustomerID)
                .Index(t => t.StatusID);
            
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        StatusID = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.StatusID);
            
            CreateTable(
                "dbo.OrderType",
                c => new
                    {
                        OrderTypeID = c.Int(nullable: false, identity: true),
                        OrderTypeDesc = c.String(),
                    })
                .PrimaryKey(t => t.OrderTypeID);
            
            CreateTable(
                "dbo.ResultsType",
                c => new
                    {
                        ResultsTypeID = c.Int(nullable: false, identity: true),
                        Desc = c.String(),
                    })
                .PrimaryKey(t => t.ResultsTypeID);
            
            CreateTable(
                "dbo.ResultFile",
                c => new
                    {
                        TestID = c.Int(nullable: false),
                        ResultsTypeID = c.Int(nullable: false),
                        FileLocation = c.String(),
                    })
                .PrimaryKey(t => new { t.TestID, t.ResultsTypeID })
                .ForeignKey("dbo.ResultsType", t => t.ResultsTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Test", t => t.TestID, cascadeDelete: true)
                .Index(t => t.TestID)
                .Index(t => t.ResultsTypeID);
            
            CreateTable(
                "dbo.Test",
                c => new
                    {
                        OrderNumber = c.Int(nullable: false),
                        OrderLine = c.Int(nullable: false),
                        TestID = c.Int(nullable: false, identity: true),
                        TestTypeID = c.Int(nullable: false),
                        TestCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ScheduledDate = c.DateTime(nullable: false),
                        CompletedDate = c.DateTime(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestID)
                .ForeignKey("dbo.Employee", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.TestType", t => t.TestTypeID, cascadeDelete: true)
                .ForeignKey("dbo.WorkOrderLine", t => new { t.OrderNumber, t.OrderLine }, cascadeDelete: true)
                .Index(t => new { t.OrderNumber, t.OrderLine })
                .Index(t => t.TestTypeID)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.WorkOrderLine",
                c => new
                    {
                        OrderNumber = c.Int(nullable: false),
                        OrderLine = c.Int(nullable: false),
                        SampleID = c.Int(nullable: false),
                        AssayID = c.Int(nullable: false),
                        LineCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.OrderNumber, t.OrderLine })
                .ForeignKey("dbo.Assay", t => t.AssayID, cascadeDelete: true)
                .ForeignKey("dbo.Sample", t => t.SampleID, cascadeDelete: true)
                .ForeignKey("dbo.WorkOrder", t => t.OrderNumber, cascadeDelete: true)
                .Index(t => t.OrderNumber)
                .Index(t => t.SampleID)
                .Index(t => t.AssayID);
            
            CreateTable(
                "dbo.Sample",
                c => new
                    {
                        SampleID = c.Int(nullable: false, identity: true),
                        CompoundID = c.Int(nullable: false),
                        ReportedQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MeasuredQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateArrived = c.DateTime(nullable: false),
                        ReceivedBy = c.Int(nullable: false),
                        ReceivingNotes = c.String(),
                        MTD = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AppearanceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SampleID)
                .ForeignKey("dbo.Appearance", t => t.AppearanceID, cascadeDelete: true)
                .ForeignKey("dbo.Compound", t => t.CompoundID, cascadeDelete: true)
                .ForeignKey("dbo.Employee", t => t.ReceivedBy, cascadeDelete: true)
                .Index(t => t.CompoundID)
                .Index(t => t.ReceivedBy)
                .Index(t => t.AppearanceID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResultFile", "TestID", "dbo.Test");
            DropForeignKey("dbo.Test", new[] { "OrderNumber", "OrderLine" }, "dbo.WorkOrderLine");
            DropForeignKey("dbo.WorkOrderLine", "OrderNumber", "dbo.WorkOrder");
            DropForeignKey("dbo.WorkOrderLine", "SampleID", "dbo.Sample");
            DropForeignKey("dbo.Sample", "ReceivedBy", "dbo.Employee");
            DropForeignKey("dbo.Sample", "CompoundID", "dbo.Compound");
            DropForeignKey("dbo.Sample", "AppearanceID", "dbo.Appearance");
            DropForeignKey("dbo.WorkOrderLine", "AssayID", "dbo.Assay");
            DropForeignKey("dbo.Test", "TestTypeID", "dbo.TestType");
            DropForeignKey("dbo.Test", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.ResultFile", "ResultsTypeID", "dbo.ResultsType");
            DropForeignKey("dbo.OrderCompound", "OrderNumber", "dbo.WorkOrder");
            DropForeignKey("dbo.WorkOrder", "OrderTypeID", "dbo.OrderType");
            DropForeignKey("dbo.WorkOrder", "StatusID", "dbo.OrderStatus");
            DropForeignKey("dbo.WorkOrder", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.OrderCompound", "CompoundID", "dbo.Compound");
            DropForeignKey("dbo.Employee", "WageTypeID", "dbo.WageType");
            DropForeignKey("dbo.Employee", "LocationID", "dbo.Location");
            DropForeignKey("dbo.Location", "StateID", "dbo.State");
            DropForeignKey("dbo.Location", "CountryID", "dbo.Country");
            DropForeignKey("dbo.Employee", "RoleID", "dbo.EmployeeRole");
            DropForeignKey("dbo.Compound", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Customer", "StateID", "dbo.State");
            DropForeignKey("dbo.Customer", "CountryID", "dbo.Country");
            DropForeignKey("dbo.AssayTest", "TestTypeID", "dbo.TestType");
            DropForeignKey("dbo.AssayTest", "AssayID", "dbo.Assay");
            DropIndex("dbo.Sample", new[] { "AppearanceID" });
            DropIndex("dbo.Sample", new[] { "ReceivedBy" });
            DropIndex("dbo.Sample", new[] { "CompoundID" });
            DropIndex("dbo.WorkOrderLine", new[] { "AssayID" });
            DropIndex("dbo.WorkOrderLine", new[] { "SampleID" });
            DropIndex("dbo.WorkOrderLine", new[] { "OrderNumber" });
            DropIndex("dbo.Test", new[] { "EmployeeID" });
            DropIndex("dbo.Test", new[] { "TestTypeID" });
            DropIndex("dbo.Test", new[] { "OrderNumber", "OrderLine" });
            DropIndex("dbo.ResultFile", new[] { "ResultsTypeID" });
            DropIndex("dbo.ResultFile", new[] { "TestID" });
            DropIndex("dbo.WorkOrder", new[] { "StatusID" });
            DropIndex("dbo.WorkOrder", new[] { "CustomerID" });
            DropIndex("dbo.WorkOrder", new[] { "OrderTypeID" });
            DropIndex("dbo.OrderCompound", new[] { "OrderNumber" });
            DropIndex("dbo.OrderCompound", new[] { "CompoundID" });
            DropIndex("dbo.Location", new[] { "CountryID" });
            DropIndex("dbo.Location", new[] { "StateID" });
            DropIndex("dbo.Employee", new[] { "LocationID" });
            DropIndex("dbo.Employee", new[] { "RoleID" });
            DropIndex("dbo.Employee", new[] { "WageTypeID" });
            DropIndex("dbo.Customer", new[] { "CountryID" });
            DropIndex("dbo.Customer", new[] { "StateID" });
            DropIndex("dbo.Compound", new[] { "CustomerID" });
            DropIndex("dbo.AssayTest", new[] { "AssayID" });
            DropIndex("dbo.AssayTest", new[] { "TestTypeID" });
            DropTable("dbo.Sample");
            DropTable("dbo.WorkOrderLine");
            DropTable("dbo.Test");
            DropTable("dbo.ResultFile");
            DropTable("dbo.ResultsType");
            DropTable("dbo.OrderType");
            DropTable("dbo.OrderStatus");
            DropTable("dbo.WorkOrder");
            DropTable("dbo.OrderCompound");
            DropTable("dbo.WageType");
            DropTable("dbo.Location");
            DropTable("dbo.Employee");
            DropTable("dbo.EmployeeRole");
            DropTable("dbo.State");
            DropTable("dbo.Country");
            DropTable("dbo.Customer");
            DropTable("dbo.Compound");
            DropTable("dbo.TestType");
            DropTable("dbo.AssayTest");
            DropTable("dbo.Assay");
            DropTable("dbo.Appearance");
        }
    }
}
