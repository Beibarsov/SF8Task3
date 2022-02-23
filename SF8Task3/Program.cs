// See https://aka.ms/new-console-template for more information

List<DirectoryInfo> AllDirs = new List<DirectoryInfo>();

Console.WriteLine("Введите путь к требуемой для очищения папке");
string path = Console.ReadLine();

long sum1;
long sum2;

DirectoryInfo directoryInfo = new DirectoryInfo(path);

if (!Directory.Exists(path))
{
    Console.WriteLine("Неправильно задан путь к папке");
}
else
{
    try
    {

        Console.WriteLine($"Исходный размер папки: {sum1 = GetLenght(path)}");
        FileInfo[] files = directoryInfo.GetFiles();
        foreach (FileInfo file in files)
        {
           // Console.WriteLine(file.FullName);
           //Console.WriteLine(file.LastAccessTime);
            if (DateTime.Now > file.LastAccessTime.AddMinutes(30))
            {
                //Console.WriteLine("Файл давно не использовался и будет удален");
                file.Delete();
            }
        }


        DirectoryInfo[] dirs = directoryInfo.GetDirectories();
        foreach (DirectoryInfo dir in dirs)
        {
            //Console.WriteLine(dir.FullName);
            //Console.WriteLine(dir.LastAccessTime);
            if (DateTime.Now > dir.LastAccessTime.AddMinutes(30))
            {
              //  Console.WriteLine("Папка давно не использовалась и будет удалена");
                dir.Delete(true);
            }
        }
        Console.WriteLine($"После очистки папка весит {sum2 = GetLenght(path)}");
        Console.WriteLine($"Освобождено {sum1 - sum2}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка: {ex}");
    }

}

long GetLenght(string path)
{
    if (!Directory.Exists(path))
    {
        Console.WriteLine("Такой директории не существует, соответственно её вес - 0");
        return 0;
    }
    long sum = 0;
    DirectoryInfo dir = new DirectoryInfo(path);
    AllDirs.Clear();
    AllDirs.Add(dir);
    GetAllDirectories(dir);

    try
    {
        foreach (DirectoryInfo d in AllDirs)
        {

            FileInfo[] files = d.GetFiles();
            foreach (FileInfo f in files)
            {
               // Console.WriteLine(f.FullName);
              //  Console.WriteLine(f.Length);
                sum += f.Length;
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
    return sum;
}

void GetAllDirectories(DirectoryInfo dir)
{
    try
    {

        DirectoryInfo[] dirs = dir.GetDirectories();
        foreach (DirectoryInfo d in dirs)
        {
            AllDirs.Add(d);
            GetAllDirectories(d);

        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}
