//-----------------------------------------------------------------------
// <copyright file="ExtantSessionFactory.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using Extant.Data.Entities;
using Extant.Data.Listeners;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using NHibernate;
using NHibernate.Event;
using NHibernate.Search.Event;

namespace Extant.Data
{
    public static class ExtantSessionFactory
    {
        /// <summary>
        /// Search config from http://www.d80.co.uk/post/2011/03/04/Nhibernate-Search-Tutorial-with-LuceneNet-and-NHibernate-30.aspx
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="luceneFolder"></param>
        /// <returns></returns>
        public static ISessionFactory GetSessionFactory(string connectionString, string luceneFolder)
        {
            var config = GetConfig(connectionString).BuildConfiguration();
            config.SetProperty("hibernate.search.default.directory_provider",
                               "NHibernate.Search.Store.FSDirectoryProvider, NHibernate.Search");
            config.SetProperty("hibernate.search.default.indexBase", luceneFolder);
            config.SetProperty("hibernate.search.default.indexing_strategy",
                               "event");
            config.SetListener(ListenerType.PreUpdate, new StudyPreUpdateListener());
            config.SetListener(ListenerType.PostUpdate, new FullTextIndexEventListener());
            config.SetListener(ListenerType.PostInsert, new FullTextIndexEventListener());
            config.SetListener(ListenerType.PostDelete, new FullTextIndexEventListener());
            return config.BuildSessionFactory();
        }

        public static FluentConfiguration GetConfig(string connectionString)
        {
            return Fluently.Configure()
                           .Database(MsSqlConfiguration.MsSql2005.ConnectionString(x => x.Is(connectionString)))
                           .Mappings(
                                x => x.AutoMappings.Add(AutoMap.AssemblyOf<User>(new ExtantAutomappingConfiguration())
                                                               .UseOverridesFromAssemblyOf<User>()
                                                               .Conventions.Add(DefaultCascade.All())
                                                               .Conventions.Add<ExtantPrimaryKeyConvention>()
                                                               .Conventions.Add<ExtantForeignKeyConvention>()
                                                               .Conventions.Add<ExtantEnumConvention>()));
        }
    }

    public class ExtantPrimaryKeyConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            instance.Column(instance.EntityType.Name + "Id");
        }
    }

    public class ExtantForeignKeyConvention : ForeignKeyConvention
    {
        protected override string GetKeyName(Member property, Type type)
        {
            if (property == null)
                return type.Name + "Id";
            return property.Name + "Id";

        }
    }

    public class ExtantAutomappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(System.Type type)
        {
            return type.Namespace == "Extant.Data.Entities" && !type.IsEnum;
        }
    }

    public class ExtantEnumConvention : IUserTypeConvention
    {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(e => e.Property.PropertyType.IsEnum || 
                (e.Property.PropertyType.IsGenericType &&
                 e.Property.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) &&
                    Nullable.GetUnderlyingType(e.Property.PropertyType).IsEnum));
        }

        public void Apply(IPropertyInstance instance)
        {
            instance.CustomType(instance.Property.PropertyType);
        }
    }

    public class UserOverride : IAutoMappingOverride<User>
    {
        public void Override(AutoMapping<User> mapping)
        {
            mapping.HasManyToMany(u => u.Roles)
                   .AsList(x => x.Column("UserIndex"))
                   .Cascade.None()
                   .Table("UserRoles");

            mapping.HasManyToMany(u => u.DiseaseAreas)
                   .Cascade.None()
                   .Table("UserDiseaseAreas");

            mapping.Map(u => u.Email).Unique();
        }
    }

    public class StudyOverride : IAutoMappingOverride<Study>
    {
        public void Override(AutoMapping<Study> mapping)
        {
            mapping.Map(s => s.Description).Length(4000);
            mapping.Map(s => s.ContactAddress).Length(4000);
            mapping.Map(s => s.StudyAdded).Not.Nullable();
            mapping.References(s => s.Owner)
                   .Cascade.None();
            mapping.HasManyToMany(s => s.Editors)
                   .Table("StudyEditors")
                   .Cascade.None();
            mapping.HasManyToMany(s => s.DiseaseAreas)
                   .Table("StudyDiseaseAreas")
                   .Cascade.None();
            mapping.HasMany(s => s.DataItems)
                   .Cascade.AllDeleteOrphan();
            mapping.HasMany(s => s.Publications)
                   .AsList(x => x.Column("StudyIndex"))
                   .Cascade.AllDeleteOrphan();
            mapping.HasMany(s => s.Samples)
                   .AsList(x => x.Column("StudyIndex"))
                   .Cascade.AllDeleteOrphan();
            mapping.HasMany(s => s.AdditionalDocuments)
                   .AsList(x => x.Column("StudyIndex"))
                   .Cascade.AllDeleteOrphan();
            mapping.HasMany(s => s.TimePoints)
                   .AsList(x => x.Column("StudyIndex"))
                   .Cascade.AllDeleteOrphan();
        }
    }

    public class DataItemOverride : IAutoMappingOverride<DataItem>
    {
        public void Override(AutoMapping<DataItem> mapping)
        {
            mapping.Map(di => di.DataItemName).Unique().Not.Nullable();
        }
    }

    public class CategoryOverride : IAutoMappingOverride<Category>
    {
        public void Override(AutoMapping<Category> mapping)
        {
            mapping.HasMany(c => c.Subcategories)
                   .KeyColumn("ParentId")
                   .AsList(x => x.Column("ParentIndex"))
                   .Cascade.AllDeleteOrphan();

            mapping.HasMany(s => s.DataItems)
                   .AsList(x => x.Column("CategoryIndex"))
                   .Cascade.AllDeleteOrphan();
        }
    }

    public class CategoryDataItemOverride : IAutoMappingOverride<CategoryDataItem>
    {
        public void Override(AutoMapping<CategoryDataItem> mapping)
        {
            mapping.References(cdi => cdi.DataItem)
                   .Cascade.SaveUpdate();
        }
    }

    public class FileUploadOverride : IAutoMappingOverride<FileUpload>
    {
        public void Override(AutoMapping<FileUpload> mapping)
        {
            mapping.Map(f => f.FileData).Length(8001); //maps to varbinary(max)
        }
    }

    public class PublicationOverride : IAutoMappingOverride<Publication>
    {
        public void Override(AutoMapping<Publication> mapping)
        {
            mapping.HasMany(p => p.MeshTerms)
                   .AsList(x => x.Column("PublicationIndex"))
                   .Element("MeshTerm")
                   .Table("PublicationMeshTerms");
            mapping.HasMany(p => p.Authors)
                   .AsList(x => x.Column("PublicationIndex"))
                   .Element("Author")
                   .Table("PublicationAuthors"); 
            mapping.Map(p => p.Title).Length(1000);
        }
    }

    public class DiseaseAreaOverride : IAutoMappingOverride<DiseaseArea>
    {
        public void Override(AutoMapping<DiseaseArea> mapping)
        {
            mapping.HasMany(da => da.Categories)
                   .AsList(x => x.Column("DiseaseAreaIndex"))
                   .Cascade.AllDeleteOrphan();
        }
    }

    public class SampleOverride : IAutoMappingOverride<Sample>
    {
        public void Override(AutoMapping<Sample> mapping)
        {
            mapping.Map(s => s.TissueSamplesPreserved).CustomType<TissueSamplesPreserved?>();
            mapping.Map(s => s.Concentration).CustomType<Concentration?>();
            mapping.Map(s => s.NumberOfAliquots).CustomType<NumberOfAliquots?>();
            mapping.Map(s => s.SnapFrozen).CustomType<YesNoUnknown?>();
            mapping.Map(s => s.HowDnaExtracted).CustomType<HowDnaExtracted?>();
            mapping.Map(s => s.TimeBetweenCollectionAndStorage).CustomType<TimeBetweenCollectionAndStorage?>();
            mapping.Map(s => s.DnaExtracted).CustomType<YesNoUnknown?>();
            mapping.Map(s => s.CellsGrown).CustomType<YesNoUnknown?>();
        }
    }

    public class StudyDataItemOverride : IAutoMappingOverride<StudyDataItem>
    {
        public void Override(AutoMapping<StudyDataItem> mapping)
        {
            mapping.References(sdi => sdi.DataItem).Cascade.SaveUpdate().Not.Nullable();
            mapping.HasManyToMany(sdi => sdi.TimePoints)
                   .Table("StudyDataItemTimePoints")
                   .Cascade.None();
        }
    }

}