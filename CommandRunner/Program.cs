using FileSystemCommands;

var testDir1 = Path.Combine(Path.GetTempPath(), "TestDir");

Directory.CreateDirectory(testDir1);

File.WriteAllText(Path.Combine(testDir1, "test1.txt"), "Hello");
File.WriteAllText(Path.Combine(testDir1, "test2.txt"), "World");

var command1 = new DirectorySizeCommand(testDir1);
command1.Execute();

Directory.Delete(testDir1, true);

var testDir = Path.Combine(Path.GetTempPath(), "TestDir");

Directory.CreateDirectory(testDir);

File.WriteAllText(Path.Combine(testDir, "file1.txt"), "Text");
File.WriteAllText(Path.Combine(testDir, "file2.log"), "Log");

var command = new FindFilesCommand(testDir, "*.txt");
command.Execute();

Directory.Delete(testDir, true);
