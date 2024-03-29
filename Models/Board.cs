﻿using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace Connect4Game.Models
{
    public class Board
    {
        public static readonly Board EmptyBoard = new Board();

        private readonly CellStates[,] cells;
        private readonly int numberOfEmptyCells;

        private int rowIndex;
        private int colIndex;

        /// <summary>
        /// Instantiates CellStates with empty cells
        /// </summary>
        private Board()
        {
            cells = new CellStates[BoardUtils.ROWS, BoardUtils.COLS];
            numberOfEmptyCells = BoardUtils.ROWS * BoardUtils.COLS;
        }

        /// <summary>
        /// Checks cell states in the matrix
        /// </summary>
        /// <param name="matrix"></param>
        public Board(int[,] matrix)
        {
            cells = new CellStates[BoardUtils.ROWS, BoardUtils.COLS];
            numberOfEmptyCells = BoardUtils.ROWS * BoardUtils.COLS;
            for (int i = 0; i < BoardUtils.ROWS; i++)
            {
                for (int j = 0; j < BoardUtils.COLS; j++)
                {
                    cells[i, j] = matrix[i, j] switch
                    {
                        0 => CellStates.EMPTY,
                        1 => CellStates.FIRST,
                        2 => CellStates.SECOND
                    };
                }
            }
        }

        /// <summary>
        /// Updates cells based from value passed in board
        /// </summary>
        /// <param name="board"></param>
        /// <param name="numberOfEmptyCells"></param>
        private Board(Board board, int numberOfEmptyCells)
        {
            if (board == null)
                throw new ArgumentNullException("board");

            if (numberOfEmptyCells < 0 || numberOfEmptyCells > BoardUtils.ROWS * BoardUtils.COLS)
                throw new ArgumentOutOfRangeException("numberOfEmptyCells");

            cells = new CellStates[BoardUtils.ROWS, BoardUtils.COLS];

            if (board != null)
            {
                for (int i = 0; i < BoardUtils.ROWS; i++)
                {
                    for (int j = 0; j < BoardUtils.COLS; j++)
                    {
                        cells[i, j] = board.cells[i, j];
                    }
                }
            }

            this.rowIndex = 0;
            this.colIndex = 0;
            this.numberOfEmptyCells = numberOfEmptyCells;
        }

        /// <summary>
        /// readonly property which returns number of empty cells
        /// </summary>
        public int NumberOfEmptyCells
        {
            get
            {
                return numberOfEmptyCells;
            }
        }

        /// <summary>
        /// gets the row index from the board
        /// </summary>
        /// <returns>Returns the row index</returns>
        public int GetRowIndex()
        {
            return this.rowIndex;
        }

        /// <summary>
        /// gets the column index from the board
        /// </summary>
        /// <returns></returns>
        public int GetColIndex()
        {
            return this.colIndex;
        }

        /// <summary>
        /// After getting column and row index it sets up the rows according to the values associated with the cellstates enum
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns>Double dimensional array which represents the board with different cell state values</returns>
        public CellStates GetCellState(int row, int column)
        {
            if (row < 0 || row >= BoardUtils.ROWS)
                throw new ArgumentOutOfRangeException("row");

            if (column < 0 || column >= BoardUtils.COLS) throw new ArgumentOutOfRangeException("column");

            return cells[row, column];
        }

        /// <summary>
        /// This modifies <see cref="board"/>, <see cref="cells"/>, <see cref="rowIndex"/>, and <see cref="colIndex"/> if move is valid.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="column"></param>
        /// <param name="board"></param>
        /// <returns>Returns true if move is successful.</returns>
        public bool MakeMove(ActivePlayer player, int column, out Board board)
        {
            if (column < 0 || column > BoardUtils.COLS - 1)
            {
                board = this;
                return false;
            }
                
            // Checks if column is occupied
            if (cells[0, column] != CellStates.EMPTY)
            {
                board = this;
                return false;
            }

            board = new Board(this, numberOfEmptyCells - 1);

            int i;

            for (i = BoardUtils.ROWS - 1; i > -1; i--)
            {
                if (cells[i, column] == CellStates.EMPTY)
                    break;
            }

            board.rowIndex = i;
            board.colIndex = column;
            board.cells[i, column] = (CellStates)player;
            //MessageBox.Show("Board: " + i + ", " + column);
            return true;
        }

    }
}