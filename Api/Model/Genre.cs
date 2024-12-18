﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Model;

[Table("genre")]
[Index(nameof(Name), IsUnique = true)]
public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<BookGenre> BookGenres { get; set; } = [];

}