using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Vasilev.SimpleChat.WpfNetCore.Client.ViewModels.Base
{
    /// <summary>
    /// Базовый класс для ViewModels
    /// </summary>
    internal abstract class ViewModelBase : INotifyPropertyChanged/*, IDisposable*/
    {
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // [CallerMemberName] - атрибут, заставляет компилятор автоматически добавлять имя метода, 
            // который будет вызывать OnPropertyChanged и записывать в propertyName имя вызываемого метода

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            // аналог:
            //PropertyChangedEventHandler handler = this.PropertyChanged;
            //if (handler != null)
            //{
            //    handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            //}
        }


        /// <summary>
        /// Сеттер. Задача - обновлять значение свойства, для которого определено поле
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">ссылка на поле свойства</param>
        /// <param name="value">новое значение</param>
        /// <param name="propertyName">имя свойства</param>
        /// <returns></returns>
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) { return false; }
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }


        //#region IDISPOSABLE

        //private bool _disposed;

        /////// <summary>
        /////// Деструктор (если вдруг)
        /////// </summary>
        ////~ViewModelBase()
        ////{
        ////    Dispose(false);
        ////}

        //public void Dispose()
        //{
        //    Dispose(true);
        //}



        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposing || _disposed) { return; }

        //    _disposed = true;
        //    // Освобождение управляемых ресурсов

        //}

        //#endregion


    }

}
