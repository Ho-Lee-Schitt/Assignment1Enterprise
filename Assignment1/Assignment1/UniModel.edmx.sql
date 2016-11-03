
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/29/2016 23:46:29
-- Generated from EDMX file: G:\Enterprise\Assignment1\Assignment1\UniModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Assignment1];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CourseStudent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Students] DROP CONSTRAINT [FK_CourseStudent];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentStudentModuleGrade]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StudentModuleGrades] DROP CONSTRAINT [FK_StudentStudentModuleGrade];
GO
IF OBJECT_ID(N'[dbo].[FK_ModuleStudentModuleGrade]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StudentModuleGrades] DROP CONSTRAINT [FK_ModuleStudentModuleGrade];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseCourseModule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CourseModules] DROP CONSTRAINT [FK_CourseCourseModule];
GO
IF OBJECT_ID(N'[dbo].[FK_LecturerModule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Modules] DROP CONSTRAINT [FK_LecturerModule];
GO
IF OBJECT_ID(N'[dbo].[FK_ModuleCourseModule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CourseModules] DROP CONSTRAINT [FK_ModuleCourseModule];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Students]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Students];
GO
IF OBJECT_ID(N'[dbo].[Lecturers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Lecturers];
GO
IF OBJECT_ID(N'[dbo].[Courses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Courses];
GO
IF OBJECT_ID(N'[dbo].[Modules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Modules];
GO
IF OBJECT_ID(N'[dbo].[CourseModules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CourseModules];
GO
IF OBJECT_ID(N'[dbo].[StudentModuleGrades]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StudentModuleGrades];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Students'
CREATE TABLE [dbo].[Students] (
    [StudentId] int IDENTITY(1,1) NOT NULL,
    [StudentForename] nvarchar(max)  NOT NULL,
    [StudentSurname] nvarchar(max)  NOT NULL,
    [StudentNumber] nvarchar(max)  NOT NULL,
    [StudentEmail] nvarchar(max)  NOT NULL,
    [CourseCourseId] int  NOT NULL
);
GO

-- Creating table 'Lecturers'
CREATE TABLE [dbo].[Lecturers] (
    [LecturerStaffNumber] int IDENTITY(1,1) NOT NULL,
    [LecturerForename] nvarchar(max)  NOT NULL,
    [LecturerSurname] nvarchar(max)  NOT NULL,
    [LecturerEmail] nvarchar(max)  NOT NULL,
    [LecturerContactNumber] bigint  NOT NULL,
    [LecturerRoom] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Courses'
CREATE TABLE [dbo].[Courses] (
    [CourseId] int IDENTITY(1,1) NOT NULL,
    [CourseName] nvarchar(max)  NOT NULL,
    [CourseCode] int  NOT NULL
);
GO

-- Creating table 'Modules'
CREATE TABLE [dbo].[Modules] (
    [ModuleId] int IDENTITY(1,1) NOT NULL,
    [ModuleName] nvarchar(max)  NOT NULL,
    [ModuleCode] nvarchar(max)  NOT NULL,
    [LecturerLecturerStaffNumber] int  NOT NULL
);
GO

-- Creating table 'CourseModules'
CREATE TABLE [dbo].[CourseModules] (
    [CourseModuleId] int IDENTITY(1,1) NOT NULL,
    [CourseCourseId] int  NOT NULL,
    [ModuleModuleId] int  NOT NULL
);
GO

-- Creating table 'StudentModuleGrades'
CREATE TABLE [dbo].[StudentModuleGrades] (
    [StudentModuleGradeId] int IDENTITY(1,1) NOT NULL,
    [StudentStudentId] int  NOT NULL,
    [ModuleModuleId] int  NOT NULL,
    [StudentGrade] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [StudentId] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [PK_Students]
    PRIMARY KEY CLUSTERED ([StudentId] ASC);
GO

-- Creating primary key on [LecturerStaffNumber] in table 'Lecturers'
ALTER TABLE [dbo].[Lecturers]
ADD CONSTRAINT [PK_Lecturers]
    PRIMARY KEY CLUSTERED ([LecturerStaffNumber] ASC);
GO

-- Creating primary key on [CourseId] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [PK_Courses]
    PRIMARY KEY CLUSTERED ([CourseId] ASC);
GO

-- Creating primary key on [ModuleId] in table 'Modules'
ALTER TABLE [dbo].[Modules]
ADD CONSTRAINT [PK_Modules]
    PRIMARY KEY CLUSTERED ([ModuleId] ASC);
GO

-- Creating primary key on [CourseModuleId] in table 'CourseModules'
ALTER TABLE [dbo].[CourseModules]
ADD CONSTRAINT [PK_CourseModules]
    PRIMARY KEY CLUSTERED ([CourseModuleId] ASC);
GO

-- Creating primary key on [StudentModuleGradeId] in table 'StudentModuleGrades'
ALTER TABLE [dbo].[StudentModuleGrades]
ADD CONSTRAINT [PK_StudentModuleGrades]
    PRIMARY KEY CLUSTERED ([StudentModuleGradeId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CourseCourseId] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [FK_CourseStudent]
    FOREIGN KEY ([CourseCourseId])
    REFERENCES [dbo].[Courses]
        ([CourseId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseStudent'
CREATE INDEX [IX_FK_CourseStudent]
ON [dbo].[Students]
    ([CourseCourseId]);
GO

-- Creating foreign key on [StudentStudentId] in table 'StudentModuleGrades'
ALTER TABLE [dbo].[StudentModuleGrades]
ADD CONSTRAINT [FK_StudentStudentModuleGrade]
    FOREIGN KEY ([StudentStudentId])
    REFERENCES [dbo].[Students]
        ([StudentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentStudentModuleGrade'
CREATE INDEX [IX_FK_StudentStudentModuleGrade]
ON [dbo].[StudentModuleGrades]
    ([StudentStudentId]);
GO

-- Creating foreign key on [ModuleModuleId] in table 'StudentModuleGrades'
ALTER TABLE [dbo].[StudentModuleGrades]
ADD CONSTRAINT [FK_ModuleStudentModuleGrade]
    FOREIGN KEY ([ModuleModuleId])
    REFERENCES [dbo].[Modules]
        ([ModuleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModuleStudentModuleGrade'
CREATE INDEX [IX_FK_ModuleStudentModuleGrade]
ON [dbo].[StudentModuleGrades]
    ([ModuleModuleId]);
GO

-- Creating foreign key on [CourseCourseId] in table 'CourseModules'
ALTER TABLE [dbo].[CourseModules]
ADD CONSTRAINT [FK_CourseCourseModule]
    FOREIGN KEY ([CourseCourseId])
    REFERENCES [dbo].[Courses]
        ([CourseId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseCourseModule'
CREATE INDEX [IX_FK_CourseCourseModule]
ON [dbo].[CourseModules]
    ([CourseCourseId]);
GO

-- Creating foreign key on [LecturerLecturerStaffNumber] in table 'Modules'
ALTER TABLE [dbo].[Modules]
ADD CONSTRAINT [FK_LecturerModule]
    FOREIGN KEY ([LecturerLecturerStaffNumber])
    REFERENCES [dbo].[Lecturers]
        ([LecturerStaffNumber])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LecturerModule'
CREATE INDEX [IX_FK_LecturerModule]
ON [dbo].[Modules]
    ([LecturerLecturerStaffNumber]);
GO

-- Creating foreign key on [ModuleModuleId] in table 'CourseModules'
ALTER TABLE [dbo].[CourseModules]
ADD CONSTRAINT [FK_ModuleCourseModule]
    FOREIGN KEY ([ModuleModuleId])
    REFERENCES [dbo].[Modules]
        ([ModuleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModuleCourseModule'
CREATE INDEX [IX_FK_ModuleCourseModule]
ON [dbo].[CourseModules]
    ([ModuleModuleId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------