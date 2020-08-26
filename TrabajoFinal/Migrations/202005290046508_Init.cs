namespace TrabajoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        DepartmentId = c.Int(nullable: false),
                        InstructorId = c.Int(nullable: false),
                        Capacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Instructor", t => t.InstructorId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.InstructorId);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Instructor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        HireDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentCourse",
                c => new
                    {
                        Student_Id = c.Int(nullable: false),
                        Course_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_Id, t.Course_Id })
                .ForeignKey("dbo.Student", t => t.Student_Id, cascadeDelete: true)
                .ForeignKey("dbo.Course", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.Student_Id)
                .Index(t => t.Course_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentCourse", "Course_Id", "dbo.Course");
            DropForeignKey("dbo.StudentCourse", "Student_Id", "dbo.Student");
            DropForeignKey("dbo.Course", "InstructorId", "dbo.Instructor");
            DropForeignKey("dbo.Course", "DepartmentId", "dbo.Department");
            DropIndex("dbo.StudentCourse", new[] { "Course_Id" });
            DropIndex("dbo.StudentCourse", new[] { "Student_Id" });
            DropIndex("dbo.Course", new[] { "InstructorId" });
            DropIndex("dbo.Course", new[] { "DepartmentId" });
            DropTable("dbo.StudentCourse");
            DropTable("dbo.Student");
            DropTable("dbo.Instructor");
            DropTable("dbo.Department");
            DropTable("dbo.Course");
        }
    }
}
