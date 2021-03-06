﻿using System.ComponentModel.DataAnnotations;
using SmartHouse.Core.Dto;

namespace SmartHouse.Web.Models
{
    public class SensorDataViewModel
    {
        [Required]
        public int? RoomId { get; set; }

        [Required]
        public int? SensorId { get; set; }

        [Required]
        public int? Value { get; set; }

        public SensorData ToSensorData()
        {
            return new SensorData
            {
                RoomId = this.RoomId,
                SensorId = this.SensorId,
                Value = this.Value
            };
        }
    }
}