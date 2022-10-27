namespace OOP_LAB_1 {
    public class Cat {
        public string Name;
        public double Age;
        public readonly int Id;

        public Gender Gender;
        public CatType Type;

        public Cat(string name, int id, double age, Gender gender, CatType type) {
            Name = name;
            Id = id;
            Age = age;
            Gender = gender;
            Type = type;
        }

        public static void Purr() {
            Console.WriteLine("Purr...");
        }
        public static void Meow() {
            Console.WriteLine("Meow!");
        }

        public override string ToString() => $"Cat(name: {Name}, age: {Age} year(s), gender: {Gender}, type: {Type})";
    }
}