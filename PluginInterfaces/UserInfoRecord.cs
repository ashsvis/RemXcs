using System;

namespace Points.Plugins
{
    [Serializable]
    sealed public class Пользователь : IComparable, ICloneable
    {
        public string Фамилия { get; set; }
        public string Имя { get; set; }
        public string Отчество { get; set; }
        public UserLevel Категория { get; set; }
        public string Пароль { get; set; }

        public Пользователь()
        {
            this.Фамилия = String.Empty;
            this.Имя = String.Empty;
            this.Отчество = String.Empty;
            this.Категория = UserLevel.Диспетчер;
            this.Пароль = String.Empty;
        }

        public string ПолноеИмя()
        {
            return String.Format("{0} {1} {2}", this.Фамилия, this.Имя, this.Отчество);
        }

        public string ФИО()
        {
            return String.Format("{0} {1}.{2}.", this.Фамилия, this.Имя[0], this.Отчество[0]);
        }

        int IComparable.CompareTo(object obj)
        {
            Пользователь user = obj as Пользователь;
            if (user != null)
                return String.Compare(this.ПолноеИмя(), user.ПолноеИмя());
            else
                throw new ArgumentException("Объект не является типом 'Пользователь'");
        }

        object ICloneable.Clone()
        {
            Пользователь obj = new Пользователь();
            obj.Фамилия = this.Фамилия;
            obj.Имя = this.Имя;
            obj.Отчество = this.Отчество;
            obj.Категория = this.Категория;
            obj.Пароль = this.Пароль;
            return obj;
        }

        public Пользователь Clone()
        {
            return (Пользователь)this.MemberwiseClone();
        }

        public void CopyFrom(Пользователь source)
        {
            this.Фамилия = source.Фамилия;
            this.Имя = source.Имя;
            this.Отчество = source.Отчество;
            this.Категория = source.Категория;
            this.Пароль = source.Пароль;
        }
    }
}
