using System;

namespace UITestProject.Common
{
    /// <summary>
    /// Результат получаемый со страницы поисковика
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Name;
        /// <summary>
        /// Ссылка
        /// </summary>
        public Uri Link;
        /// <summary>
        /// Краткое описание
        /// </summary>
        public string Description;
    }
}
