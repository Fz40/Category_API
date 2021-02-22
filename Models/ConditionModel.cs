#nullable disable

namespace API.Models
{
    public partial class ConditionModel
    {
        public string encrypt { get; set; }
        public int id { get; set; }
        public string sid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public void clear()
        {
            this.encrypt = "";
            this.id = 0;
            this.Name = "";
            this.Description = "";
        }

    }
}