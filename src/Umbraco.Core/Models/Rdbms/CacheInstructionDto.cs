﻿using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Umbraco.Core.Models.Rdbms
{
    [TableName("umbracoCacheInstruction")]
    [PrimaryKey("id")]
    [ExplicitColumns]
    internal class CacheInstructionDto
    {
        [Column("id")]
        [NullSetting(NullSetting = NullSettings.NotNull)]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("utcStamp")]
        [NullSetting(NullSetting = NullSettings.NotNull)]
        public DateTime UtcStamp { get; set; }

        [Column("jsonInstruction")]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        [NullSetting(NullSetting = NullSettings.NotNull)]
        public string JsonInstruction { get; set; }


    }
}