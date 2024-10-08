﻿// Connect to the TaskQueue
using System;
using System.Drawing;

namespace DistributedQueue.Common
{
    public class ConsoleAdapter
    {


        public Size Size { get; set; }

        public void Write(string value)
        {
            Console.Write(value);
        }

        public void Write(char value)
        {
            Console.Write(value);
        }

        public void WriteLine(string? value = null)
        {
            Console.WriteLine(value);
        }

        public void MoveCursor(int left, int top)
        {
            if (left <= Size.Width && top <= Size.Height)
            {
                Console.SetCursorPosition(left, top);
            }

        }

        public ConsoleColor ForegroundColor
        {
            get => Console.ForegroundColor;
            set => Console.ForegroundColor = value;
        }

        public ConsoleColor BackgroundColor
        {
            get => Console.BackgroundColor;
            set =>
                    Console.BackgroundColor = value;
        }
        public bool CursorVisible { get; set; }

        public void MoveArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
        {
            //Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop);
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }

        public void Beep()
        {
            Console.Beep();
        }

    }
}