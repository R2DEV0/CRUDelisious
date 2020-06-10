using System.ComponentModel.DataAnnotations;
using System;

namespace CRUDlisious.Models
{
    public class Dish
    {
        [Key]
        public int DishId {get; set;}


        [Required(ErrorMessage="Dish name is required")]
        [MinLength(2, ErrorMessage="Dish must have at least 2 characters")]
        public string Name {get; set;}


        [Required(ErrorMessage="There must be a dish chef")]
        [MinLength(2, ErrorMessage="Chef must have at least 2 characters in name")]
        public string Chef {get; set;}


        [Required(ErrorMessage="You must have a tastiness level")]
        public int? Taste {get; set;}


        [Required(ErrorMessage="You must have a calorie count")]
        public int? Calories {get; set;}


        [Required(ErrorMessage="You must have a description")]
        [MinLength(10, ErrorMessage="Description must have at least 10 characters")]
        public string Description {get; set;}


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}