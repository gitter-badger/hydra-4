USE [SDKInterfaceLibrary]
GO

/*
	Table: tblSDKHeaderPackExpansionType

	Generated automatically by DiaHeadersSqlTransformer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=09006a76b6b1ba0d, Author: u164225, Date: 10/13/2016 4:38:28 PM
	Maps to class VisualStudioProvider.PDB.Headers.PackExpansionType

	<References>
		<Reference>HeaderFile</Reference>
		<Reference>Type</Reference>
	</References>
*/


CREATE TABLE [dbo].[tblSDKHeaderPackExpansionType](
		[PackExpansionTypeId] [uniqueidentifier] NOT NULL,
		[HeaderFileId] [uniqueidentifier] NOT NULL,
		[TypeId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_tblSDKHeaderPackExpansionType] PRIMARY KEY CLUSTERED
(
	[PackExpansionTypeId] ASC
)

WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO
