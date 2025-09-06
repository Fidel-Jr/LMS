namespace LmsProject.Models
{
    public class ModuleMaterial
    {
        public int ModuleId { get; set; }
        public Module Module { get; set; }
        public int MaterialId { get; set; }
        public Material Material { get; set; }

    }
}
