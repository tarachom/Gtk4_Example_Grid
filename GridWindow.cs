using Gtk;
using static Gtk.Orientation;

class GridWindow : Window
{
    enum Column
    {
        Caption,
        Field,
        Help
    }

    ComboBoxText comboBoxUser;
    PasswordEntry password;

    public GridWindow(Application? app) : base()
    {
        Application = app;
        Title = "Авторизація";
        Resizable = false;
        Modal = true;

        Box vBox = Box.New(Vertical, 0);
        vBox.MarginTop = vBox.MarginBottom = vBox.MarginStart = vBox.MarginEnd = 10;
        Child = vBox;

        Grid grid = Grid.New();
        grid.ColumnSpacing = grid.RowSpacing = 10;
        vBox.Append(grid);

        int row = 0;

        //Користувач
        {
            Label labelCaption = Label.New("Користувач:");
            labelCaption.Halign = Align.End;
            grid.Attach(labelCaption, (int)Column.Caption, row, 1, 1);

            comboBoxUser = new ComboBoxText();
            comboBoxUser.Append("user1", "User 1");
            comboBoxUser.Append("user2", "User 2");

            grid.Attach(comboBoxUser, (int)Column.Field, row, 1, 1);

            Label labelHelp = Label.New("Виберіть користувача");
            labelHelp.Halign = Align.Start;

            grid.Attach(labelHelp, (int)Column.Help, row, 1, 1);
        }

        //Пароль
        {
            row++;

            Label labelCaption = Label.New("Пароль:");
            labelCaption.Halign = Align.End;
            grid.Attach(labelCaption, (int)Column.Caption, row, 1, 1);

            password = new PasswordEntry() { WidthRequest = 300, ShowPeekIcon = true };
            grid.Attach(password, (int)Column.Field, row, 1, 1);

            Label labelHelp = Label.New("Введіть пароль");
            labelHelp.Halign = Align.Start;

            grid.Attach(labelHelp, (int)Column.Help, row, 1, 1);
        }

        Separator separator = Separator.New(Vertical);
        separator.MarginTop = separator.MarginBottom = 10;
        vBox.Append(separator);

        //Кнопки
        {
            Box hBox = Box.New(Horizontal, 0);
            hBox.Halign = Align.Center;
            vBox.Append(hBox);

            {
                Button button = Button.NewWithLabel("Авторизація");
                button.MarginStart = button.MarginEnd = 3;
                button.AddCssClass("ok");
                button.OnClicked += (_, _) =>
                {
                    string? user = comboBoxUser.ActiveId;
                    string? pass = password.Text_;

                    if (!string.IsNullOrEmpty(user) && pass != null)
                    {
                        // Перевірка пароля
                    }
                    else
                        Message.Error(Application, this, "Помилка", "Не заповнені поля!");
                };

                hBox.Append(button);
            }

            {
                Button button = Button.NewWithLabel("Відмінити");
                button.MarginStart = button.MarginEnd = 3;
                button.AddCssClass("ok");
                button.OnClicked += (_, _) => Close();
                hBox.Append(button);
            }
        }
    }

    class Message()
    {
        public static void Error(Application? app, Window? win, string text, string? secondaryText = null)
        {
            MessageDialog message = new()
            {
                TransientFor = win,
                Application = app,
                Modal = true,
                Valign = Align.Center,
                Halign = Align.Center,
                Text = text,
                SecondaryText = secondaryText
            };

            message.AddButton("Закрити", 1);

            message.OnResponse += (_, _) =>
            {
                message.Hide();
                message.Destroy();
            };

            message.Show();
        }
    }
}