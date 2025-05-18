namespace OmidProject.Infrastructures.CommandDb;

public abstract class BaseRepository
{
    protected readonly OmidProjectCommandDb _Db;
    protected readonly OmidProjectBaseCommandDb _DbBase;

    protected BaseRepository(OmidProjectCommandDb db)
    {
        _Db = db;
    }

    protected BaseRepository(OmidProjectCommandDb db, OmidProjectBaseCommandDb dbBase)
    {
        _Db = db;
        _DbBase = dbBase;
    }

    protected BaseRepository(OmidProjectBaseCommandDb dbBase)
    {
        _DbBase = dbBase;
    }
}