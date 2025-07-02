using CommandLib;

namespace FileSystemCommands;

public class DirectorySizeCommand : ICommand
{
    public string Name;
    public long size = 0;
    
    public DirectorySizeCommand(string name)
    {
        Name = name;
    }
    public void Execute()
    {

        foreach (string file in Directory.GetFiles(Name))
        {
            if (File.Exists(file))
            {
                FileInfo f = new FileInfo(file);
                size += f.Length;
            }
        }
        Console.WriteLine(size);
    }
}

public class FindFilesCommand : ICommand
{
    public string Name;
    public string Mask;
    public int countFile = 0;
    public FindFilesCommand(string name, string mask)
    {
        Name = name;
        Mask = mask;
    }
    public void Execute()
    {
        foreach (string file in Directory.GetFiles(Name, Mask))
        {
            if (File.Exists(file))
            {
                FileInfo f = new FileInfo(file);
                countFile += 1;
                Console.WriteLine(f.Name);
            }
        }
    }
}
