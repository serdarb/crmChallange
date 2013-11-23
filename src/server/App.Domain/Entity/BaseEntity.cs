using System;
using MongoDB.Bson;

namespace App.Domain
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedAt = UpdatedAt = DateTime.Now;
        }

        public ObjectId Id { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedAtYear
        {
            get { return CreatedAt.Year; }
            set
            {

            }

        }
        public int CreatedAtMonth
        {
            get { return CreatedAt.Month; }
            set
            {

            }
        }

        public int CreatedAtDay
        {
            get { return CreatedAt.Day; }
            set
            {

            }
        }

        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        public bool IsDeleted { get; set; }

        public string IdStr
        {
            get
            {
                return Id.ToString();
            }
        }
    }
}
