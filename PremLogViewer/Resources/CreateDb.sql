USE MASTER
GO
CREATE DATABASE ExceptionLog
GO
USE ExceptionLog
GO
CREATE TABLE LogFile (
	Idx INT PRIMARY KEY NOT NULL IDENTITY,
	CustomerName NVARCHAR(50) NOT NULL,
	SourceFileName NVARCHAR(50) NOT NULL,
	SourceFileHash NVARCHAR(64) NOT NULL,
	ProcessedDate DATETIME NOT NULL)
GO
CREATE TABLE LogEntry (
	Idx INT PRIMARY KEY NOT NULL IDENTITY,
	BatchIdx INT NOT NULL,
	LineData NVARCHAR(MAX) NOT NULL,
	ErrorNo INT,
	HeaderTimestamp DATETIME,
	ExceptionType NVARCHAR(255),
	GmtTimeStamp DATETIME,
	Message NVARCHAR(2000),
	Data NVARCHAR(2000),
	AppDomainName NVARCHAR(255),
	WindowsIdentity NVARCHAR(255))
GO
CREATE PROC CreateBatch
(
	@customerName NVARCHAR(50),
	@sourceFileName NVARCHAR(50),
	@sourceFileHash NVARCHAR(64)
	)
AS
BEGIN
DECLARE @logFileIdx INT
DECLARE @preExists BIT

	IF EXISTS (SELECT * FROM LogFile WHERE SourceFileHash = @sourceFileHash) BEGIN

		SELECT @logFileIdx = Idx FROM LogFile WHERE SourceFileHash = @sourceFileHash
		SET @preExists = 1
	END
	ELSE BEGIN
		INSERT INTO LogFile (CustomerName, SourceFileName, SourceFileHash, ProcessedDate)
			VALUES (@customerName, @sourceFileName, @sourceFileHash, GETDATE())
			
		SELECT @logFileIdx = @@IDENTITY
		SET @preExists = 0
	END

	SELECT @logFileIdx, @preExists
		
END
GO

CREATE PROC InsertLogEntry (
	@logFileIdx INT,
	@lineData NVARCHAR(MAX),
	@errorNo INT,
	@headerTimestamp DATETIME,
	@exceptionType NVARCHAR(255),
	@gmtTimestamp DATETIME,
	@message NVARCHAR(2000),
	@data NVARCHAR(2000),
	@appDomainName NVARCHAR(255),
	@windowsIdentityName NVARCHAR(255)
	)
AS
BEGIN
	INSERT INTO LogEntry (BatchIdx, LineData, ErrorNo, HeaderTimestamp,ExceptionType, GmtTimeStamp, Message, Data, AppDomainName, WindowsIdentity)
		VALUES (@logFileIdx, @lineData, @errorNo, @headerTimestamp, @exceptionType, @gmtTimestamp, @message, @data, @appDomainName, @windowsIdentityName)
		
	SELECT @@IDENTITY	
	
END
GO
