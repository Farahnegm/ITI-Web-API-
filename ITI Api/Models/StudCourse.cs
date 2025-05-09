﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ITI_Api.Models;

[PrimaryKey("CrsId", "StId")]
[Table("Stud_Course")]
public partial class StudCourse
{
    [Key]
    [Column("Crs_Id")]
    public int CrsId { get; set; }

    [Key]
    [Column("St_Id")]
    public int StId { get; set; }

    public int? Grade { get; set; }

    [ForeignKey("CrsId")]
    [InverseProperty("StudCourses")]
    public virtual Course Crs { get; set; }

    [ForeignKey("StId")]
    [InverseProperty("StudCourses")]
    public virtual Student St { get; set; }
}