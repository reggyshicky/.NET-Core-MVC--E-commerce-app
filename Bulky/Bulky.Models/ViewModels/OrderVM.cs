using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models.ViewModels
{
	public class OrderVM
	{
		public OrderHeader OrderHeader{get; set;}
		public IEnumerable<OrderDetail> OrderDetail { get; set; }
	}
}

//In a computer program, A viewModel is a special class where you can write down all the information you want to show on a 
// or an app screen. This information can be things like a person's name, address, or a list of items they bought in an online store
//so a viewModel is a way to organize and store data thay you want to display or use in your computer program
