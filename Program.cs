Console.WriteLine("demo v1");

var charset = Enumerable.Range(0, 127).Select(x => (char)x)
.Where(x => char.IsLetterOrDigit(x) || char.IsPunctuation(x))
.ToList();


