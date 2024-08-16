using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.ViewModels
{
    public class CreateContributionViewModel
    {
        public IEnumerable<Member> Members { get; set; }
        public IEnumerable<Festival> Festivals { get; set; }
        [Required(ErrorMessage = "Please enter the amount.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }
        public int? MemberId { get; set; }
        public int? FestivalId { get; set; }
    }
}
