﻿#region licence
// =====================================================
// EfSchemeCompare Project - project to compare EF schema to SQL schema
// Filename: Test60CompareSqlAndSql.cs
// Date Created: 2016/04/06
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// =====================================================
#endregion

using System;
using System.Configuration;
using Ef6SchemaCompare;
using Ef6TestDbContext;
using NUnit.Framework;
using Tests.Helpers;

namespace Tests.UnitTests
{
    public class Test60CompareSqlAndSql
    {
        [Test]
        public void Test01CompareSqlToSqlSelfOk()
        {
            //SETUP
            var connection = ConfigurationManager.ConnectionStrings[MiscConstants.GetEfDatabaseConfigName()].ConnectionString;
            var comparer = new CompareSqlSql();

            //EXECUTE
            var status = comparer.CompareSqlToSql(connection, connection);

            //VERIFY
            status.ShouldBeValid();
            status.HasWarnings.ShouldEqual(false, string.Join("\n", status.Warnings));
        }

        [Test]
        public void Test10CompareSqlToSqlDbUpIndexAsWarningsOk()
        {
            //SETUP
            var connection1 = ConfigurationManager.ConnectionStrings[MiscConstants.GetEfDatabaseConfigName()].ConnectionString;
            var connection2 = ConfigurationManager.ConnectionStrings[MiscConstants.DbUpDatabaseConfigName].ConnectionString;
            var comparer = new CompareSqlSql(false);

            //EXECUTE
            var status = comparer.CompareSqlToSql( connection1, connection2);

            //VERIFY
            status.ShouldBeValid();
            foreach (var warning in status.Warnings)
            {
                Console.WriteLine(warning);
            }
            //status.HasWarnings.ShouldEqual(false, string.Join("\n", status.Warnings));
        }

        [Test]
        public void Test11CompareSqlToSqlDbUpOk()
        {
            //SETUP
            var connection1 = ConfigurationManager.ConnectionStrings[MiscConstants.GetEfDatabaseConfigName()].ConnectionString;
            var connection2 = ConfigurationManager.ConnectionStrings[MiscConstants.DbUpDatabaseConfigName].ConnectionString;
            var comparer = new CompareSqlSql();

            //EXECUTE
            var status = comparer.CompareSqlToSql(connection1, connection2);

            //VERIFY
            status.ShouldBeValid();
            status.HasWarnings.ShouldEqual(false, string.Join("\n", status.Warnings));
        }


        [Test]
        public void Test20CompareSqlToSqlDbUpWithConfigLookupOk()
        {
            //SETUP
            var comparer = new CompareSqlSql();

            //EXECUTE
            var status = comparer.CompareSqlToSql(MiscConstants.GetEfDatabaseConfigName(), MiscConstants.DbUpDatabaseConfigName);

            //VERIFY
            status.ShouldBeValid();
            status.HasWarnings.ShouldEqual(false, string.Join("\n", status.Warnings));
        }

        [Test]
        public void Test30CompareEfGeneratedSqlToSqlOk()
        {
            using (var db = new TestEf6SchemaCompareDb())
            {
                //SETUP
                var comparer = new CompareSqlSql();

                //EXECUTE
                var status = comparer.CompareEfGeneratedSqlToSql(db, MiscConstants.DbUpDatabaseConfigName);

                //VERIFY
                status.ShouldBeValid();
                status.HasWarnings.ShouldEqual(false, string.Join("\n", status.Warnings));
            }
        }
    }
}