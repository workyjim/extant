
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK5B8924C5F835844]') AND parent_object_id = OBJECT_ID('[AdditionalDocument]'))
alter table [AdditionalDocument]  drop constraint FK5B8924C5F835844


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK5B8924C424B1889]') AND parent_object_id = OBJECT_ID('[AdditionalDocument]'))
alter table [AdditionalDocument]  drop constraint FK5B8924C424B1889


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK6482F249ABC5A65]') AND parent_object_id = OBJECT_ID('[Category]'))
alter table [Category]  drop constraint FK6482F249ABC5A65


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK6482F246EF32120]') AND parent_object_id = OBJECT_ID('[Category]'))
alter table [Category]  drop constraint FK6482F246EF32120


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK6AA17F3C5983C99D]') AND parent_object_id = OBJECT_ID('[CategoryDataItem]'))
alter table [CategoryDataItem]  drop constraint FK6AA17F3C5983C99D


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK6AA17F3C22221AA2]') AND parent_object_id = OBJECT_ID('[CategoryDataItem]'))
alter table [CategoryDataItem]  drop constraint FK6AA17F3C22221AA2


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKDFFCD148424B1889]') AND parent_object_id = OBJECT_ID('[Publication]'))
alter table [Publication]  drop constraint FKDFFCD148424B1889


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKCBCBEC826680909B]') AND parent_object_id = OBJECT_ID('PublicationMeshTerms'))
alter table PublicationMeshTerms  drop constraint FKCBCBEC826680909B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK6D4AA56B6680909B]') AND parent_object_id = OBJECT_ID('PublicationAuthors'))
alter table PublicationAuthors  drop constraint FK6D4AA56B6680909B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2C37DAB8424B1889]') AND parent_object_id = OBJECT_ID('[Sample]'))
alter table [Sample]  drop constraint FK2C37DAB8424B1889


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK346A35EDA27EE18E]') AND parent_object_id = OBJECT_ID('[Study]'))
alter table [Study]  drop constraint FK346A35EDA27EE18E


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK346A35ED7A11CECD]') AND parent_object_id = OBJECT_ID('[Study]'))
alter table [Study]  drop constraint FK346A35ED7A11CECD


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK346A35EDDDC77B22]') AND parent_object_id = OBJECT_ID('[Study]'))
alter table [Study]  drop constraint FK346A35EDDDC77B22


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK346A35ED27E0DB6C]') AND parent_object_id = OBJECT_ID('[Study]'))
alter table [Study]  drop constraint FK346A35ED27E0DB6C


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK60F0F7CC24403DF6]') AND parent_object_id = OBJECT_ID('StudyEditors'))
alter table StudyEditors  drop constraint FK60F0F7CC24403DF6


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK60F0F7CC424B1889]') AND parent_object_id = OBJECT_ID('StudyEditors'))
alter table StudyEditors  drop constraint FK60F0F7CC424B1889


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2BD316196EF32120]') AND parent_object_id = OBJECT_ID('StudyDiseaseAreas'))
alter table StudyDiseaseAreas  drop constraint FK2BD316196EF32120


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2BD31619424B1889]') AND parent_object_id = OBJECT_ID('StudyDiseaseAreas'))
alter table StudyDiseaseAreas  drop constraint FK2BD31619424B1889


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK434D32AD5983C99D]') AND parent_object_id = OBJECT_ID('[StudyDataItem]'))
alter table [StudyDataItem]  drop constraint FK434D32AD5983C99D


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK434D32AD424B1889]') AND parent_object_id = OBJECT_ID('[StudyDataItem]'))
alter table [StudyDataItem]  drop constraint FK434D32AD424B1889


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKA01480843F06EA8]') AND parent_object_id = OBJECT_ID('StudyDataItemTimePoints'))
alter table StudyDataItemTimePoints  drop constraint FKA01480843F06EA8


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKA014808431033FF0]') AND parent_object_id = OBJECT_ID('StudyDataItemTimePoints'))
alter table StudyDataItemTimePoints  drop constraint FKA014808431033FF0


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK48CDEE9F424B1889]') AND parent_object_id = OBJECT_ID('[TimePoint]'))
alter table [TimePoint]  drop constraint FK48CDEE9F424B1889


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2A0A9B1F13636525]') AND parent_object_id = OBJECT_ID('UserRoles'))
alter table UserRoles  drop constraint FK2A0A9B1F13636525


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2A0A9B1F24403DF6]') AND parent_object_id = OBJECT_ID('UserRoles'))
alter table UserRoles  drop constraint FK2A0A9B1F24403DF6


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK5DF97E176EF32120]') AND parent_object_id = OBJECT_ID('UserDiseaseAreas'))
alter table UserDiseaseAreas  drop constraint FK5DF97E176EF32120


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK5DF97E1724403DF6]') AND parent_object_id = OBJECT_ID('UserDiseaseAreas'))
alter table UserDiseaseAreas  drop constraint FK5DF97E1724403DF6


    if exists (select * from dbo.sysobjects where id = object_id(N'[AdditionalDocument]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [AdditionalDocument]

    if exists (select * from dbo.sysobjects where id = object_id(N'[Category]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Category]

    if exists (select * from dbo.sysobjects where id = object_id(N'[CategoryDataItem]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [CategoryDataItem]

    if exists (select * from dbo.sysobjects where id = object_id(N'[DataItem]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [DataItem]

    if exists (select * from dbo.sysobjects where id = object_id(N'[DiseaseArea]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [DiseaseArea]

    if exists (select * from dbo.sysobjects where id = object_id(N'[FileUpload]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [FileUpload]

    if exists (select * from dbo.sysobjects where id = object_id(N'[Publication]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Publication]

    if exists (select * from dbo.sysobjects where id = object_id(N'PublicationMeshTerms') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table PublicationMeshTerms

    if exists (select * from dbo.sysobjects where id = object_id(N'PublicationAuthors') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table PublicationAuthors

    if exists (select * from dbo.sysobjects where id = object_id(N'[Role]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Role]

    if exists (select * from dbo.sysobjects where id = object_id(N'[Sample]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Sample]

    if exists (select * from dbo.sysobjects where id = object_id(N'[Study]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Study]

    if exists (select * from dbo.sysobjects where id = object_id(N'StudyEditors') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table StudyEditors

    if exists (select * from dbo.sysobjects where id = object_id(N'StudyDiseaseAreas') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table StudyDiseaseAreas

    if exists (select * from dbo.sysobjects where id = object_id(N'[StudyDataItem]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [StudyDataItem]

    if exists (select * from dbo.sysobjects where id = object_id(N'StudyDataItemTimePoints') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table StudyDataItemTimePoints

    if exists (select * from dbo.sysobjects where id = object_id(N'[TimePoint]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [TimePoint]

    if exists (select * from dbo.sysobjects where id = object_id(N'[User]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [User]

    if exists (select * from dbo.sysobjects where id = object_id(N'UserRoles') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table UserRoles

    if exists (select * from dbo.sysobjects where id = object_id(N'UserDiseaseAreas') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table UserDiseaseAreas

    create table [AdditionalDocument] (
        AdditionalDocumentId INT IDENTITY NOT NULL,
       Version INT not null,
       Description NVARCHAR(255) null,
       DocumentType INT null,
       FileId INT null,
       StudyId INT null,
       StudyIndex INT null,
       primary key (AdditionalDocumentId)
    )

    create table [Category] (
        CategoryId INT IDENTITY NOT NULL,
       Version INT not null,
       CategoryName NVARCHAR(255) null,
       ParentId INT null,
       ParentIndex INT null,
       DiseaseAreaId INT null,
       DiseaseAreaIndex INT null,
       primary key (CategoryId)
    )

    create table [CategoryDataItem] (
        CategoryDataItemId INT IDENTITY NOT NULL,
       Version INT not null,
       ShortName NVARCHAR(255) null,
       DataItemId INT null,
       CategoryId INT null,
       CategoryIndex INT null,
       primary key (CategoryDataItemId)
    )

    create table [DataItem] (
        DataItemId INT IDENTITY NOT NULL,
       Version INT not null,
       DataItemName NVARCHAR(255) not null unique,
       primary key (DataItemId)
    )

    create table [DiseaseArea] (
        DiseaseAreaId INT IDENTITY NOT NULL,
       Version INT not null,
       DiseaseAreaName NVARCHAR(255) null,
       DiseaseAreaSynonyms NVARCHAR(255) null,
       Published BIT null,
       primary key (DiseaseAreaId)
    )

    create table [FileUpload] (
        FileUploadId INT IDENTITY NOT NULL,
       Version INT not null,
       FileData VARBINARY(MAX) null,
       FileName NVARCHAR(255) null,
       MimeType NVARCHAR(255) null,
       FileSize INT null,
       primary key (FileUploadId)
    )

    create table [Publication] (
        PublicationId INT IDENTITY NOT NULL,
       Version INT not null,
       Title NVARCHAR(1000) null,
       Url NVARCHAR(255) null,
       Pmid INT null,
       Journal NVARCHAR(255) null,
       PublicationDate NVARCHAR(255) null,
       StudyId INT null,
       StudyIndex INT null,
       primary key (PublicationId)
    )

    create table PublicationMeshTerms (
        PublicationId INT not null,
       MeshTerm NVARCHAR(255) null,
       PublicationIndex INT not null,
       primary key (PublicationId, PublicationIndex)
    )

    create table PublicationAuthors (
        PublicationId INT not null,
       Author NVARCHAR(255) null,
       PublicationIndex INT not null,
       primary key (PublicationId, PublicationIndex)
    )

    create table [Role] (
        RoleId INT IDENTITY NOT NULL,
       Version INT not null,
       RoleName NVARCHAR(255) null,
       primary key (RoleId)
    )

    create table [Sample] (
        SampleId INT IDENTITY NOT NULL,
       Version INT not null,
       TissueSamplesPreserved INT null,
       Concentration INT null,
       NumberOfAliquots INT null,
       SnapFrozen INT null,
       HowDnaExtracted INT null,
       TimeBetweenCollectionAndStorage INT null,
       DnaExtracted INT null,
       CellsGrown INT null,
       SampleType INT null,
       SampleTypeSpecify NVARCHAR(255) null,
       NumberOfSamples INT null,
       NumberOfSamplesExact INT null,
       BioMatWholeBlood BIT null,
       BioMatBuffyCoat BIT null,
       BioMatSaliva BIT null,
       BioMatBuccalSwabs BIT null,
       BioMatAcidCitrateDextrose BIT null,
       BioMatSynovialFluid BIT null,
       BioMatSynovialTissue BIT null,
       BioMatSerumSeparatorTube BIT null,
       BioMatPlasmaSeparatorTube BIT null,
       BioMatUrine BIT null,
       BioMatOtherTubes BIT null,
       BioMatEdtaBlood BIT null,
       BioMatSalivaNoAdditive BIT null,
       BioMatSalivaOragene BIT null,
       BioMatCulture BIT null,
       BioMatUnknown BIT null,
       BioMatOther BIT null,
       BioMatOtherTubesSpecify NVARCHAR(255) null,
       BioMatCultureSpecify NVARCHAR(255) null,
       BioMatOtherSpecify NVARCHAR(255) null,
       TissueSamplesPreservedSpecify NVARCHAR(255) null,
       SampleVolume INT null,
       SampleVolumeSpecify NVARCHAR(255) null,
       CellCount INT null,
       ConcentrationSpecify NVARCHAR(255) null,
       WhenCollected INT null,
       HowDnaExtractedSpecify NVARCHAR(255) null,
       CollectionToStorageTemp INT null,
       CollectionToStorageTempSpecify NVARCHAR(255) null,
       StorageTemp INT null,
       StorageTempSpecify NVARCHAR(255) null,
       AlwayStoredAtThisTemp INT null,
       DnaQualityAbsorbance BIT null,
       DnaQualityGel BIT null,
       DnaQualityCommercialKit BIT null,
       DnaQualityPcr BIT null,
       DnaQualityPicoGreen BIT null,
       DnaQualityUnknown BIT null,
       DnaQualityOther BIT null,
       DnaQualityOtherSpecify NVARCHAR(255) null,
       FreezeThawCycles INT null,
       AnalysisNo BIT null,
       AnalysisSequencing BIT null,
       AnalysisRealTimePcr BIT null,
       AnalysisPcr BIT null,
       AnalysisGenotyping BIT null,
       AnalysisBiochemistry BIT null,
       AnalysisImmunochemistry BIT null,
       AnalysisDnaExtraction BIT null,
       AnalysisImmunohistochemistry BIT null,
       AnalysisCellLinesDerived BIT null,
       AnalysisRnaExtraction BIT null,
       AnalysisUnknown BIT null,
       AnalysisOther BIT null,
       AnalysisOtherSpecify NVARCHAR(255) null,
       StudyId INT null,
       StudyIndex INT null,
       primary key (SampleId)
    )

    create table [Study] (
        StudyId INT IDENTITY NOT NULL,
       Version INT not null,
       Description NVARCHAR(4000) null,
       ContactAddress NVARCHAR(4000) null,
       StudyAdded DATETIME not null,
       StudyUpdated DATETIME null,
       Published BIT null,
       StudyName NVARCHAR(255) null,
       StudySynonyms NVARCHAR(255) null,
       StudyWebsite NVARCHAR(255) null,
       StudyDesign INT null,
       StartDate DATETIME null,
       StudyStatus INT null,
       RecruitmentTarget INT null,
       ParticipantsRecruited INT null,
       ParticipantsRecruitedUpdated DATETIME null,
       PrincipalInvestigator NVARCHAR(255) null,
       Institution NVARCHAR(255) null,
       Funder NVARCHAR(255) null,
       OnPortfolio BIT null,
       PortfolioNumber INT null,
       ContactName NVARCHAR(255) null,
       ContactPhone NVARCHAR(255) null,
       ContactEmail NVARCHAR(255) null,
       HasDataAccessPolicy BIT null,
       IsLongitudinal BIT null,
       UseTimePoints BIT null,
       NumberOfDnaSamples INT null,
       NumberOfDnaSamplesExact INT null,
       NumberOfSerumSamples INT null,
       NumberOfSerumSamplesExact INT null,
       NumberOfPlasmaSamples INT null,
       NumberOfPlasmaSamplesExact INT null,
       NumberOfWholeBloodSamples INT null,
       NumberOfWholeBloodSamplesExact INT null,
       NumberOfSalivaSamples INT null,
       NumberOfSalivaSamplesExact INT null,
       NumberOfTissueSamples INT null,
       NumberOfTissueSamplesExact INT null,
       NumberOfCellSamples INT null,
       NumberOfCellSamplesExact INT null,
       NumberOfOtherSamples INT null,
       NumberOfOtherSamplesExact INT null,
       DetailedSampleInfo BIT null,
       OwnerId INT null,
       PatientInformationLeafletId INT null,
       ConsentFormId INT null,
       DataAccessPolicyId INT null,
       primary key (StudyId)
    )

    create table StudyEditors (
        StudyId INT not null,
       UserId INT not null
    )

    create table StudyDiseaseAreas (
        StudyId INT not null,
       DiseaseAreaId INT not null
    )

    create table [StudyDataItem] (
        StudyDataItemId INT IDENTITY NOT NULL,
       Version INT not null,
       DataItemId INT not null,
       StudyId INT null,
       primary key (StudyDataItemId)
    )

    create table StudyDataItemTimePoints (
        StudyDataItemId INT not null,
       TimePointId INT not null
    )

    create table [TimePoint] (
        TimePointId INT IDENTITY NOT NULL,
       Version INT not null,
       Name NVARCHAR(255) null,
       StudyId INT null,
       StudyIndex INT null,
       primary key (TimePointId)
    )

    create table [User] (
        UserId INT IDENTITY NOT NULL,
       Version INT not null,
       Email NVARCHAR(255) null unique,
       UserName NVARCHAR(255) null,
       Password NVARCHAR(255) null,
       Salt NVARCHAR(255) null,
       IsApproved BIT null,
       IsLockedOut BIT null,
       CreationDate DATETIME null,
       LastLoginDate DATETIME null,
       LastPasswordChangeDate DATETIME null,
       LastLockedOutDate DATETIME null,
       IncorrectPasswordWindowStart DATETIME null,
       IncorrectPasswordCount INT null,
       PasswordResetCode NVARCHAR(255) null,
       PasswordResetDate DATETIME null,
       Deleted BIT null,
       EmailValidated BIT null,
       EmailValidationCode NVARCHAR(255) null,
       primary key (UserId)
    )

    create table UserRoles (
        UserId INT not null,
       RoleId INT not null,
       UserIndex INT not null,
       primary key (UserId, UserIndex)
    )

    create table UserDiseaseAreas (
        UserId INT not null,
       DiseaseAreaId INT not null
    )

    alter table [AdditionalDocument] 
        add constraint FK5B8924C5F835844 
        foreign key (FileId) 
        references [FileUpload]

    alter table [AdditionalDocument] 
        add constraint FK5B8924C424B1889 
        foreign key (StudyId) 
        references [Study]

    alter table [Category] 
        add constraint FK6482F249ABC5A65 
        foreign key (ParentId) 
        references [Category]

    alter table [Category] 
        add constraint FK6482F246EF32120 
        foreign key (DiseaseAreaId) 
        references [DiseaseArea]

    alter table [CategoryDataItem] 
        add constraint FK6AA17F3C5983C99D 
        foreign key (DataItemId) 
        references [DataItem]

    alter table [CategoryDataItem] 
        add constraint FK6AA17F3C22221AA2 
        foreign key (CategoryId) 
        references [Category]

    alter table [Publication] 
        add constraint FKDFFCD148424B1889 
        foreign key (StudyId) 
        references [Study]

    alter table PublicationMeshTerms 
        add constraint FKCBCBEC826680909B 
        foreign key (PublicationId) 
        references [Publication]

    alter table PublicationAuthors 
        add constraint FK6D4AA56B6680909B 
        foreign key (PublicationId) 
        references [Publication]

    alter table [Sample] 
        add constraint FK2C37DAB8424B1889 
        foreign key (StudyId) 
        references [Study]

    alter table [Study] 
        add constraint FK346A35EDA27EE18E 
        foreign key (OwnerId) 
        references [User]

    alter table [Study] 
        add constraint FK346A35ED7A11CECD 
        foreign key (PatientInformationLeafletId) 
        references [FileUpload]

    alter table [Study] 
        add constraint FK346A35EDDDC77B22 
        foreign key (ConsentFormId) 
        references [FileUpload]

    alter table [Study] 
        add constraint FK346A35ED27E0DB6C 
        foreign key (DataAccessPolicyId) 
        references [FileUpload]

    alter table StudyEditors 
        add constraint FK60F0F7CC24403DF6 
        foreign key (UserId) 
        references [User]

    alter table StudyEditors 
        add constraint FK60F0F7CC424B1889 
        foreign key (StudyId) 
        references [Study]

    alter table StudyDiseaseAreas 
        add constraint FK2BD316196EF32120 
        foreign key (DiseaseAreaId) 
        references [DiseaseArea]

    alter table StudyDiseaseAreas 
        add constraint FK2BD31619424B1889 
        foreign key (StudyId) 
        references [Study]

    alter table [StudyDataItem] 
        add constraint FK434D32AD5983C99D 
        foreign key (DataItemId) 
        references [DataItem]

    alter table [StudyDataItem] 
        add constraint FK434D32AD424B1889 
        foreign key (StudyId) 
        references [Study]

    alter table StudyDataItemTimePoints 
        add constraint FKA01480843F06EA8 
        foreign key (TimePointId) 
        references [TimePoint]

    alter table StudyDataItemTimePoints 
        add constraint FKA014808431033FF0 
        foreign key (StudyDataItemId) 
        references [StudyDataItem]

    alter table [TimePoint] 
        add constraint FK48CDEE9F424B1889 
        foreign key (StudyId) 
        references [Study]

    alter table UserRoles 
        add constraint FK2A0A9B1F13636525 
        foreign key (RoleId) 
        references [Role]

    alter table UserRoles 
        add constraint FK2A0A9B1F24403DF6 
        foreign key (UserId) 
        references [User]

    alter table UserDiseaseAreas 
        add constraint FK5DF97E176EF32120 
        foreign key (DiseaseAreaId) 
        references [DiseaseArea]

    alter table UserDiseaseAreas 
        add constraint FK5DF97E1724403DF6 
        foreign key (UserId) 
        references [User]
