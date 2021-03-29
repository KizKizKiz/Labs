using System;
using System.Windows.Forms;

namespace DbIntegrationApp
{
    public static class ErrorProviderExtension
    {
        public static void ShowErrorIfNotValid<T>(this ErrorProvider errorProvider, T entity, Func<bool> condition, Action whenInValid, string message)
            where T : Control
        {
            if (errorProvider is null)
            {
                throw new ArgumentNullException(nameof(errorProvider));
            }
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            if (whenInValid is null)
            {
                throw new ArgumentNullException(nameof(whenInValid));
            }

            if (condition())
            {
                errorProvider.SetError(entity, message);
                whenInValid();
            }
            else
            {
                errorProvider.SetError(entity, string.Empty);
            }
        }
    }
}
