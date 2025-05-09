﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ITI_Api.Models;

[PrimaryKey("InsId", "CrsId")]
[Table("Ins_Course")]
public partial class InsCourse
{
    [Key]
    [Column("Ins_Id")]
    public int InsId { get; set; }

    [Key]
    [Column("Crs_Id")]
    public int CrsId { get; set; }

    [StringLength(50)]
    public string Evaluation { get; set; }

    [ForeignKey("CrsId")]
    [InverseProperty("InsCourses")]
    public virtual Course Crs { get; set; }

    [ForeignKey("InsId")]
    [InverseProperty("InsCourses")]
    public virtual Instructor Ins { get; set; }
}