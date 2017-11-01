USE MASTER
GO
CREATE DATABASE ExceptionLog
GO
USE ExceptionLog
GO
CREATE TABLE LogFile (
	Idx INT PRIMARY KEY NOT NULL IDENTITY,	
	SourceFileName NVARCHAR(50) NOT NULL,
	SourceFileHash NVARCHAR(64) NOT NULL
)

GO
CREATE TABLE LogEntry (
	Idx INT PRIMARY KEY NOT NULL IDENTITY,
	BatchIdx INT NOT NULL FOREIGN KEY REFERENCES Batch(Idx),
	FileIdx INT NOT NULL FOREIGN KEY REFERENCES LogFile(Idx),
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

CREATE TABLE Batch (
	Idx INT PRIMARY KEY NOT NULL IDENTITY,
	TechName NVARCHAR(50) NOT NULL,
	CustomerName NVARCHAR(50) NOT NULL,
	ImportedDate DATETIME NOT NULL,

)
GO

CREATE PROC LogHashCheck
(
	@sourceFileName NVARCHAR(50),
	@sourceFileHash NVARCHAR(64)
)
AS
BEGIN
DECLARE @logFileIdx INT
DECLARE @preExists BIT
DECLARE @batchIdx INT

	IF EXISTS (SELECT * FROM LogFile WHERE SourceFileHash = @sourceFileHash) BEGIN

		SELECT @logFileIdx = Idx FROM LogFile WHERE SourceFileHash = @sourceFileHash
		SET @preExists = 1
	END
	ELSE BEGIN
		INSERT INTO LogFile (SourceFileName, SourceFileHash)
			VALUES (@sourceFileName, @sourceFileHash)
		
			
		SELECT @logFileIdx = @@IDENTITY
		SET @preExists = 0
	END
	
	SET @batchIdx = (Select MAX(Idx) From Batch)

	SELECT @logFileIdx, @preExists, @batchIdx
		
END
GO

CREATE PROC CreateBatch
(
	@TechName nvarchar(50),
	@customerName NVARCHAR(50)
)
As
Begin
	INSERT INTO BATCH (TechName, CustomerName, ImportedDate) Values (@TechName,@CustomerName,GETDATE())
	
End
GO

CREATE PROC InsertLogEntry (
	@logFileIdx INT,
	@BatchIdx INT,
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
	INSERT INTO LogEntry (BatchIdx, FileIdx, LineData, ErrorNo, HeaderTimestamp,ExceptionType, GmtTimeStamp, Message, Data, AppDomainName, WindowsIdentity)
		VALUES (@BatchIdx, @logFileIdx, @lineData, @errorNo, @headerTimestamp, @exceptionType, @gmtTimestamp, @message, @data, @appDomainName, @windowsIdentityName)
		
	SELECT @@IDENTITY	
	
END
GO
