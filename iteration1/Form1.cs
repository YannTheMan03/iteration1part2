using System.Drawing.Drawing2D;
using System.Timers;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
namespace iteration1
{
    /// <summary>
    /// The main class of the form
    /// </summary>
    public partial class Form1 : Form
    {
        // Game State Variables
        private bool _isGameOver = false;
        private int _scoreCount = 0;
        private int _gcCount = 0;

        // Player Variables
        private Player _player;
        private int _playerLivesLeft = 3;
        private int _livesSpacing = 0;

        // Bullet Variables
        private List<Bullet> _bullets = [];
        private List<Bullet> _disposedBullets = [];
        private Bullet _bullet;
        private const int BulletVelocity = -5;
        private const int BulletDamage = 25;
        private DateTime _lastShot = DateTime.MinValue;
        private const int FireDelay = 200;

        // Enemy Variables
        

        // User Interface Variables
        private Label _scoreLabel = new();
        private Rectangle _formBounds;
        private Image[] _background;
        private int _currentImageIndex = 0;
        private int BackgroundChangeCounter = 0;
        private const int BackgroundChangeDelay = 500;
        private DateTime _lastBackgroundChange = DateTime.Now;

        // Leaderboard Variables
        private Leaderboard _leaderboard = new();
        private static readonly string LeaderBoardPath
            = "C:\\Users\\yb.2415248\\OneDrive - Hereford Sixth Form College\\Computer Science\\C03 - Project\\Assets\\leaderboard.json";

        // Wave Variables
        private Wave currentWave;
        private int _currentWaveIndex = 1;


        // Flags
        private bool _isMovingLeft;
        private bool _isMovingRight;
        private bool _isPressingSpace;

   
        // Form Loading
        public Form1()
        {
            InitializeComponent();
            ChangeBackground();
            StartWave(_currentWaveIndex);
        }

        // Change Background Image
        void ChangeBackground()
        {
            _background = new Image[]
            {
                Properties.Resources.BG1,
                Properties.Resources.BG2,
                Properties.Resources.BG3,
                Properties.Resources.BG4,
                Properties.Resources.BG5,
                Properties.Resources.BG6,
                Properties.Resources.BG7,
                Properties.Resources.BG8

            };

            this.BackgroundImage = _background[0];
        }

        // Form Loading
        private void Form1_Load(object sender, EventArgs e)
        {
            // Formatting Screen
            this.Size = new System.Drawing.Size(400, 600);
            this.MaximizeBox = true;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.DoubleBuffered = true;
            _formBounds = ClientRectangle;

            // New Player Instance
            _player = new Player(Properties.Resources.player_one, 175, 500);

            // Load Scores
            _leaderboard.Load(LeaderBoardPath);

            // Score Label Creation
            _scoreLabel.Text = _scoreCount.ToString();
            _scoreLabel.Visible = true;
            _scoreLabel.Location = new System.Drawing.Point(this.ClientSize.Width - _scoreLabel.Width, 0);
            _scoreLabel.BackColor = System.Drawing.Color.Transparent;
            _scoreLabel.ForeColor = System.Drawing.Color.White;
            _scoreLabel.Font = new Font("Pixeloid Sans", 14);
            _scoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Controls.Add(_scoreLabel);
        }

        // Key Pressing
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // Check Keys
            if (e.KeyCode == Keys.Left) _isMovingLeft = true;
            if (e.KeyCode == Keys.Right) _isMovingRight = true;
            if (e.KeyCode == Keys.Space && _isPressingSpace == false)
            {
                _isPressingSpace = true;

                // Firing Delay
                if ((DateTime.Now - _lastShot).TotalMilliseconds > FireDelay)
                {
                    _bullet = new Bullet(Properties.Resources.bulletImage, _player.PositionX + 17, _player.PositionY);
                    _bullets.Add(_bullet);
                    _lastShot = DateTime.Now;
                }
            }
        }

        // Key Release
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            // Check Keys
            if (e.KeyCode == Keys.Space) _isPressingSpace = false;
            if (e.KeyCode == Keys.Left) _isMovingLeft = false;
            if (e.KeyCode == Keys.Right) _isMovingRight = false;
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            // Creating Pen/Graphics
            Graphics G = e.Graphics;
            Pen Pen = new Pen(Color.Red);

            // Draw Player
            G.DrawImage(_player.SpriteImage, _player.PositionX, _player.PositionY);

            // Draw Hearts
            if (_playerLivesLeft != 0)
            {
                for (int i = 1; i <= _playerLivesLeft; i++)
                {
                    G.DrawImage(Properties.Resources.heartPng, _livesSpacing, 0);
                    _livesSpacing += 30;
                }
                _livesSpacing = 0;
            }
            
            // Draw Bullets
            if (_bullets.Count > 0){
                foreach (Bullet bullet in _bullets)
                {
                    if ((bullet.HitBox).IntersectsWith(_formBounds)) G.DrawImage(bullet.SpriteImage, bullet.PositionX, bullet.PositionY);
                    else _disposedBullets.Add(bullet);
                }
                foreach (Bullet bullet in _disposedBullets) _bullets.Remove(bullet);

                _disposedBullets.Clear();
            }

            // Draw Enemies
            foreach (var enemy in currentWave.enemies.ToList())
            {
                if ((enemy.HitBox.IntersectsWith(_player.HitBox))||(enemy.PositionY >550))
                {
                    currentWave.enemies.Remove(enemy);
                    _playerLivesLeft -= 1;
                }
                currentWave?.Draw(G);
            }
            
 


        }
        
        private void OnGameTick(object sender, EventArgs e)
        {
            // Clear Memory (128 ticks)
            if (++_gcCount == 128)
            {
                GC.Collect();
                _gcCount = 0;
            }
            // Player movement
            if (_isMovingLeft && _player.PositionX > 7) _player.PositionX -= 7;
            if (_isMovingRight && _player.PositionX < _formBounds.Width - _player.SpriteImage.Width) _player.PositionX += 7;

            // Update bullet positions
            foreach (Bullet bullet in _bullets) bullet.PositionY += BulletVelocity;

            // End game
            if ((_playerLivesLeft <= 0 )||( _currentWaveIndex == 6))
            {
                game_Timer.Stop(); 
                GameOver();
            }

            // Remove Bullets
            foreach (Bullet bullet in _disposedBullets) _bullets.Remove(bullet);
            _disposedBullets.Clear();

            // Update Enemies
            
            if (currentWave != null){
                currentWave.Update();
                foreach (var bullet in _bullets.ToList())
                {
                    foreach (var enemy in currentWave.enemies.ToList())
                    {
                        if (bullet.HitBox.IntersectsWith(enemy.HitBox))
                        {
                            enemy.TakeDamage(BulletDamage);
                            _bullets.Remove(bullet);
                            _scoreCount += 10;
                            break;
                        }
                    }
                }
                foreach (var enemyBullet in currentWave.EnemyBullets.ToList())
                {
                    enemyBullet.PositionY += 2; 

                    if (enemyBullet.HitBox.IntersectsWith(_player.HitBox))
                    {
                        _playerLivesLeft--;
                        currentWave.EnemyBullets.Remove(enemyBullet);
                    }
                    else if (enemyBullet.PositionY > _formBounds.Height)
                    {
                        currentWave.EnemyBullets.Remove(enemyBullet);
                    }
                }
                if (currentWave.enemies.Count == 0)
                {
                    _currentWaveIndex++;
                    StartWave(_currentWaveIndex);
                }
            }

            // Background Change
            if ((DateTime.Now - _lastBackgroundChange).TotalMilliseconds >= BackgroundChangeDelay)
            {
                _currentImageIndex = (_currentImageIndex + 1) % _background.Length;
                this.BackgroundImage = _background[_currentImageIndex];
                _lastBackgroundChange = DateTime.Now;
            }
            _scoreLabel.Text = _scoreCount.ToString();

            // Redraw Screen
            this.Invalidate();
        }

        // End Game
        private void GameOver()
        {
            var topScores = _leaderboard.GetTopScores(5);
            string message = " Leaderboard: \n\n";

            _leaderboard.AddOrUpdateScore("Player1", _scoreCount);
            _leaderboard.Save(LeaderBoardPath);        

            foreach (var entry in topScores) message += $"{entry.Key}: {entry.Value} \n";

            MessageBox.Show(message, "Game Over");
            game_Timer.Stop();
            this.Close();
        }

        // Working on waves
        private void StartWave(int _currentWaveIndex)
        {
            currentWave = new Wave(_currentWaveIndex);
            this.Invalidate();
        }               
    }
}
