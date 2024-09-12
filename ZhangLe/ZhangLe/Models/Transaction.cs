using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ZhangLe.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeID { get; set; } = "2";

        [Required] // 确保 Amount 不为空
        public decimal Amount { get; set; }

        [DataType(DataType.Date)] // 只显示日期的年月日部分
        [Required] // 确保 Date 不为空
        public DateTime Date { get; set; } = DateTime.Now.Date; // 设置默认值为当前日期的年月日部分
    }
}
