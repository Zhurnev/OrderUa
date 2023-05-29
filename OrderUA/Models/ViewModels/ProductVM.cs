﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrderUA.Models.ViewModels
{
	public class ProductVM
	{
		public Product Product { get; set; }
		public IEnumerable<SelectListItem> CategorySelectList { get; set; }
		public IEnumerable<SelectListItem> ProductTypeSelectList { get; set; }
	}
}
