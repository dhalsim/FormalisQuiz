using System;
using System.Data.Entity;
using FormalisQuiz.Models.Interfaces;

namespace FormalisQuiz.DataLayer.Repositories
{
    public class RepositoryBase<TR> : IDisposable
      where TR : DbContext, IDisposedTracker, new()
    {
        private TR _dataContext;

        protected TR DataContext
        {
            get
            {
                if (_dataContext == null || _dataContext.IsDisposed)
                {
                    _dataContext = new TR();
                }
                return _dataContext;
            }
        }

        public void Dispose()
        {
            if (DataContext != null) DataContext.Dispose();
        }
    }
}