
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 01/29/2016 08:09:40
-- Generated from EDMX file: D:\Temp\Mitgliederdatenbank (10)\Mitgliederdatenbank\Mitgliederdatenbank\DataAccess\dbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MitgliederDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_tblAbrechnungen_tblMitglieder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblAbrechnungen] DROP CONSTRAINT [FK_tblAbrechnungen_tblMitglieder];
GO
IF OBJECT_ID(N'[dbo].[FK_tblTrainingseinheiten_tblBenutzer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblTrainingseinheiten] DROP CONSTRAINT [FK_tblTrainingseinheiten_tblBenutzer];
GO
IF OBJECT_ID(N'[dbo].[FK_tblTrainingseinheiten_tblMitglieder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblTrainingseinheiten] DROP CONSTRAINT [FK_tblTrainingseinheiten_tblMitglieder];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[tblAbrechnungen]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblAbrechnungen];
GO
IF OBJECT_ID(N'[dbo].[tblBenutzer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblBenutzer];
GO
IF OBJECT_ID(N'[dbo].[tblMitglieder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblMitglieder];
GO
IF OBJECT_ID(N'[dbo].[tblTrainingseinheiten]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblTrainingseinheiten];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tblBenutzer'
CREATE TABLE [dbo].[tblBenutzer] (
    [benIdPK] int  identity(1,1) NOT NULL,
    [benName] varchar(25)  NULL,
    [benPasswort] varchar(25)  NULL,
    [benVersion] timestamp  NULL,
    [benIstAdmin] bit  NULL
);
GO

-- Creating table 'tblMitglieder'
CREATE TABLE [dbo].[tblMitglieder] (
    [mitIdPk] int  identity(1,1) NOT NULL,
    [mitName] varchar(25)  NULL,
    [mitVorname] varchar(25)  NULL,
    [mitStraßeNr] varchar(50)  NULL,
    [mitPlzOrt] varchar(50)  NULL,
    [mitTel] varchar(50)  NULL,
    [mitEmail] varchar(50)  NULL,
    [mitGebDatum] datetime  NULL,
    [mitMtglDatum] datetime  NULL,
    [mitVersion] timestamp  NULL
);
GO

-- Creating table 'tblTrainingseinheiten'
CREATE TABLE [dbo].[tblTrainingseinheiten] (
    [trainIdPk] int  identity(1,1) NOT NULL,
    [trainMitIdFk] int  NULL,
    [trainMitName] varchar(25)  NULL,
    [trainBenIdFk] int  NULL,
    [trainBenName] varchar(25)  NULL,
    [trainDatum] datetime  NULL,
    [trainVersion] timestamp  NULL
);
GO

-- Creating table 'tblAbrechnungen'
CREATE TABLE [dbo].[tblAbrechnungen] (
    [abrIdPk] int  identity(1,1) NOT NULL,
    [abrMitIdFk] int  NULL,
    [abrMonat] datetime  NULL,
    [abrPDF] binary(50)  NULL,
    [abrVersion] timestamp  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [benIdPK] in table 'tblBenutzer'
ALTER TABLE [dbo].[tblBenutzer]
ADD CONSTRAINT [PK_tblBenutzer]
    PRIMARY KEY CLUSTERED ([benIdPK] ASC);
GO

-- Creating primary key on [mitIdPk] in table 'tblMitglieder'
ALTER TABLE [dbo].[tblMitglieder]
ADD CONSTRAINT [PK_tblMitglieder]
    PRIMARY KEY CLUSTERED ([mitIdPk] ASC);
GO

-- Creating primary key on [trainIdPk] in table 'tblTrainingseinheiten'
ALTER TABLE [dbo].[tblTrainingseinheiten]
ADD CONSTRAINT [PK_tblTrainingseinheiten]
    PRIMARY KEY CLUSTERED ([trainIdPk] ASC);
GO

-- Creating primary key on [abrIdPk] in table 'tblAbrechnungen'
ALTER TABLE [dbo].[tblAbrechnungen]
ADD CONSTRAINT [PK_tblAbrechnungen]
    PRIMARY KEY CLUSTERED ([abrIdPk] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [abrMitIdFk] in table 'tblAbrechnungen'
ALTER TABLE [dbo].[tblAbrechnungen]
ADD CONSTRAINT [FK_tblAbrechnungen_tblMitglieder]
    FOREIGN KEY ([abrMitIdFk])
    REFERENCES [dbo].[tblMitglieder]
        ([mitIdPk])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tblAbrechnungen_tblMitglieder'
CREATE INDEX [IX_FK_tblAbrechnungen_tblMitglieder]
ON [dbo].[tblAbrechnungen]
    ([abrMitIdFk]);
GO

-- Creating foreign key on [trainBenIdFk] in table 'tblTrainingseinheiten'
ALTER TABLE [dbo].[tblTrainingseinheiten]
ADD CONSTRAINT [FK_tblTrainingseinheiten_tblBenutzer]
    FOREIGN KEY ([trainBenIdFk])
    REFERENCES [dbo].[tblBenutzer]
        ([benIdPK])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tblTrainingseinheiten_tblBenutzer'
CREATE INDEX [IX_FK_tblTrainingseinheiten_tblBenutzer]
ON [dbo].[tblTrainingseinheiten]
    ([trainBenIdFk]);
GO

-- Creating foreign key on [trainMitIdFk] in table 'tblTrainingseinheiten'
ALTER TABLE [dbo].[tblTrainingseinheiten]
ADD CONSTRAINT [FK_tblTrainingseinheiten_tblMitglieder]
    FOREIGN KEY ([trainMitIdFk])
    REFERENCES [dbo].[tblMitglieder]
        ([mitIdPk])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tblTrainingseinheiten_tblMitglieder'
CREATE INDEX [IX_FK_tblTrainingseinheiten_tblMitglieder]
ON [dbo].[tblTrainingseinheiten]
    ([trainMitIdFk]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------