namespace chess
{
    public partial class ChessGame : Form
    {
        private Dictionary<(int col, int row), PictureBox> figureContainers = new Dictionary<(int col, int row), PictureBox>();
        private Dictionary<string, Image> pieceImages = new Dictionary<string, Image>();
        private PictureBox selectedFigure = null;

        public ChessGame()
        {
            InitializeComponent();
            tableLayoutPanel1.BackgroundImageLayout = ImageLayout.Stretch;
            LoadPieceImages();
            SetupPieces("Белые"); // По умолчанию белые фигуры

        }

        // Загрузка картинок фигур
        private void LoadPieceImages()
        {
            string[] names = { "Pawn", "Horse", "Officer", "Queen", "King", "Rook"};
            string[] colors = { "W", "B" };
            foreach (var n in names)
                foreach (var c in colors)
                {
                    string key = n + c;
                    string path = Path.Combine("C:\\Users\\bogdf\\source\\repos\\chess\\Images\\", key + ".png");
                    if (File.Exists(path))
                        pieceImages[key] = Image.FromFile(path);
                }
        }

        // Удаляет фигуры со всех клеток (удаляет PictureBox из TableLayoutPanel)
        private void ClearBoardFigures()
        {
            // Собираем PictureBox, чтобы не удалять чужие контроли
            List<Control> toRemove = new List<Control>();
            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                if (ctrl is PictureBox)
                    toRemove.Add(ctrl);
            }
            foreach (var ctrl in toRemove)
                tableLayoutPanel1.Controls.Remove(ctrl);
        }

        // Размещает фигуру на доске
        private void PlaceFigure(int col, int row, string pieceKey)
        {
            if (!pieceImages.ContainsKey(pieceKey))
                return;

            // Создаём PictureBox для фигуры
            PictureBox pb = new PictureBox
            {
                Image = pieceImages[pieceKey],
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            // Добавляем обработчик кликов
            pb.Click += PictureBox_Click;

            // Сначала удалим, если уже что-то есть (например, при перезапуске игры)
            var prev = tableLayoutPanel1.GetControlFromPosition(col, row);
            if (prev is PictureBox)
            {
                tableLayoutPanel1.Controls.Remove(prev);
                figureContainers.Remove((col, row));
            }

            // Добавляем PictureBox в TableLayoutPanel и контейнер
            tableLayoutPanel1.Controls.Add(pb, col, row);
            figureContainers[(col, row)] = pb;
        }

        // Расстановка фигур в зависимости от выбранной стороны
        private void SetupPieces(string side)
        {
            ClearBoardFigures();

            bool isWhite = side == "Белые";
            int mainRowWhite = 7, pawnRowWhite = 6;
            int mainRowBlack = 0, pawnRowBlack = 1;

            // Если игрок за черных — разворачиваем доску
            if (!isWhite)
            {
                mainRowWhite = 0; pawnRowWhite = 1;
                mainRowBlack = 7; pawnRowBlack = 6;
            }

            // Белые фигуры
            PlaceFigure(0, mainRowWhite, "RookW");
            PlaceFigure(1, mainRowWhite, "HorseW");
            PlaceFigure(2, mainRowWhite, "OfficerW");
            PlaceFigure(3, mainRowWhite, "QueenW");
            PlaceFigure(4, mainRowWhite, "KingW");
            PlaceFigure(5, mainRowWhite, "OfficerW");
            PlaceFigure(6, mainRowWhite, "HorseW");
            PlaceFigure(7, mainRowWhite, "RookW");
            for (int col = 0; col < 8; col++)
                PlaceFigure(col, pawnRowWhite, "PawnW");

            // Черные фигуры
            PlaceFigure(0, mainRowBlack, "RookB");
            PlaceFigure(1, mainRowBlack, "HorseB");
            PlaceFigure(2, mainRowBlack, "OfficerB");
            PlaceFigure(3, mainRowBlack, "QueenB");
            PlaceFigure(4, mainRowBlack, "KingB");
            PlaceFigure(5, mainRowBlack, "OfficerB");
            PlaceFigure(6, mainRowBlack, "HorseB");
            PlaceFigure(7, mainRowBlack, "RookB");
            for (int col = 0; col < 8; col++)
                PlaceFigure(col, pawnRowBlack, "PawnB");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void PictureBox_Click(object sender, EventArgs e)
        {
            var clickedFigure = sender as PictureBox;

            // Сбрасываем подсветку предыдущей фигуры
            if (selectedFigure != null)
            {
                selectedFigure.BackColor = Color.Transparent;
            }

            // Подсвечиваем выбранную фигуру
            selectedFigure = clickedFigure;
            selectedFigure.BackColor = Color.Red; // Цвет подсветки
        }

        // Получение фигуры по координатам
        private PictureBox GetFigureAtPosition(int col, int row)
        {
            return figureContainers.TryGetValue((col, row), out var pb) ? pb : null;
        }

    }
}
