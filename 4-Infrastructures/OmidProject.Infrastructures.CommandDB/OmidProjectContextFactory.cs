using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OmidProject.Infrastructures.CommandDb;

public class OmidProjectContextFactory : IDesignTimeDbContextFactory<OmidProjectBaseCommandDb>
{
    public OmidProjectBaseCommandDb CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OmidProjectBaseCommandDb>();
        optionsBuilder.UseSqlServer(
            @"Data Source=192.168.92.69;Initial Catalog=OmidProject;User Id=sa;Password=123;Trusted_Connection=false;Trust Server Certificate=True;");

        return new OmidProjectBaseCommandDb(optionsBuilder.Options);
    }
}