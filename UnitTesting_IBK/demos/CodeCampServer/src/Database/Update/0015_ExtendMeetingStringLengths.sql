﻿/*
Run this script on:

        localhost\sqlexpress.CodeCampServerVersioned    -  This database will be modified

to synchronize it with:

        localhost\sqlexpress.CodeCampServer

You are recommended to back up your database before running this script

Script created by SQL Compare version 8.1.0 from Red Gate Software Ltd at 9/22/2009 9:36:36 AM

*/
SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
IF EXISTS (SELECT * FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#tmpErrors')) DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
BEGIN TRANSACTION
GO
PRINT N'Altering [dbo].[Meetings]'
GO
ALTER TABLE [dbo].[Meetings] ALTER COLUMN [Topic] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
ALTER TABLE [dbo].[Meetings] ALTER COLUMN [Summary] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
ALTER TABLE [dbo].[Meetings] ALTER COLUMN [SpeakerBio] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT 'The database update succeeded'
COMMIT TRANSACTION
END
ELSE PRINT 'The database update failed'
GO
DROP TABLE #tmpErrors
GO
