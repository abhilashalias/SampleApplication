USE DemoApplication;


CREATE TABLE [Audits] (
    [AuditId] int NOT NULL IDENTITY,
    [Particulars] nvarchar(256) NOT NULL,
    [Accountability] nvarchar(256) NOT NULL,
    [Capex] nvarchar(50) NOT NULL,
    [Priority] nvarchar(50) NOT NULL,
    [Comments] nvarchar(1000) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Audits] PRIMARY KEY ([AuditId])
);
GO

