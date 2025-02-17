USE [db_EmployeeManagement_Project_WithAJAX_New]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employees] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]  NVARCHAR (MAX) NOT NULL,
    [Address]    NVARCHAR (MAX) NOT NULL,
    [Gender]     NVARCHAR (MAX) NOT NULL,
    [Email]      NVARCHAR (MAX) NOT NULL,
    [Contact]    NVARCHAR (MAX) NOT NULL,
    [TeamId]     INT            NOT NULL,
    [PositionId] INT            NOT NULL,
	[DepartmentId] INT            NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Employees_PositionId]
    ON [dbo].[Employees]([PositionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Employees_TeamId]
    ON [dbo].[Employees]([TeamId] ASC);

	GO
CREATE NONCLUSTERED INDEX [IX_Employees_DepartmentId]
    ON [dbo].[Employees]([DepartmentId] ASC);



GO
ALTER TABLE [dbo].[Employees]
    ADD CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([Id] ASC);


GO
ALTER TABLE [dbo].[Employees]
    ADD CONSTRAINT [FK_Employees_Positions_PositionId] FOREIGN KEY ([PositionId]) REFERENCES [dbo].[Positions] ([Id]) ON DELETE CASCADE;


GO
ALTER TABLE [dbo].[Employees]
    ADD CONSTRAINT [FK_Employees_Teams_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Teams] ([Id]) ON DELETE CASCADE;


GO
ALTER TABLE [dbo].[Employees]
    ADD CONSTRAINT [FK_Employees_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Departments] ([Id]) ON DELETE CASCADE;