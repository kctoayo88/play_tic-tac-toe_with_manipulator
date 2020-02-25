// A Hello World! program in C#.
using System;
using System.Collections.Generic;


namespace TicTacToe
{
    public class Game 
    {
        static int random8() {
            Random r = new Random();
            return r.Next(0,9);
        }
        static bool is_cross(int value) {
            return value == 2;
        }
        static bool is_circle(int value) {
            return value == 1;
        }
        static bool is_empty(int value) {
            return value == 0;
        }
        static int[][] get_lines() {
            int[][] lines =  {
                new int [] {0,1,2},
                new int [] {3,4,5},
                new int [] {6,7,8},
                new int [] {0,3,6},
                new int [] {1,4,7},
                new int [] {2,5,8},
                new int [] {0,4,8},
                new int [] {2,4,6}
            };
            return lines;
        }
        static bool is_cross_win(int[] chessboard) {
            int[][] lines = get_lines();
            bool is_win = false;
            foreach (int[] line in lines) {
                is_win = true;
                foreach (int pos in line) {
                    if (!is_cross(chessboard[pos])) {
                        is_win = false;
                    }
                }
                if (is_win) {
                    break;
                }
            }
            return is_win;
        }
        static bool is_circle_win(int[] chessboard) {
            int[][] lines = get_lines();
            bool is_win = false;
            foreach (int[] line in lines) {
                is_win = true;
                foreach (int pos in line) {
                    if (!is_circle(chessboard[pos])) {
                        is_win = false;
                    }
                }
                if (is_win) {
                    break;
                }
            }
            return is_win;
        }
        static bool is_draw(int[] chessboard) {
            int[][] lines = get_lines();
            bool is_every_line_has_both_cross_circle = true;
            foreach (int[] line in lines) {
                bool has_cross = false;
                bool has_circle = false;
                foreach (int pos in line) {
                    if (is_cross(chessboard[pos])) {
                        has_cross = true;
                    } else if (is_circle(chessboard[pos])) {
                        has_circle = true;
                    }
                }
                bool has_both_cross_circle = has_cross && has_circle;
                if (!has_both_cross_circle) {
                    is_every_line_has_both_cross_circle = false;
                    break;
                }
            }
            return is_every_line_has_both_cross_circle;
        }
        static bool is_playing(int[] chessboard) {
            return true;
        }
        static int get_cross_count(int[] chessboard) {
            int count = 0;
            for (int pos = 0; pos < 9; ++pos) {
                if (is_cross(chessboard[pos])) {
                    count += 1;
                }
            }
            return count;
        }
        static int get_circle_count(int[] chessboard) {
            int count = 0;
            for (int pos = 0; pos < 9; ++pos) {
                if (is_circle(chessboard[pos])) {
                    count += 1;
                }
            }
            return count;
        }
        static int get_empty_count(int[] chessboard) {
            int count = 0;
            for (int pos = 0; pos < 9; ++pos) {
                if (is_empty(chessboard[pos])) {
                    count += 1;
                }
            }
            return count;
        }
        static bool is_invalid(int[] chessboard) {
            int cross_count = get_cross_count(chessboard);
            int circle_count = get_circle_count(chessboard);
            int empty_count = get_empty_count(chessboard);
            int total_count = cross_count + circle_count + empty_count;
            if (total_count != 9) {
                return true;
            }
            int circle_cross_difference = circle_count - cross_count;
            if (circle_cross_difference != 0 && circle_cross_difference != 1) {
                return true;
            }
            return false;
        }
        public static int get_game_state(int[] chessboard) {
            if (is_invalid(chessboard)) {
                return -4;
            } else if (is_draw(chessboard)) {
                return -3;
            } else if (is_circle_win(chessboard)) {
                return -1;
            } else if (is_cross_win(chessboard)) {
                return -2;
            } else {
                return 0;
            }
        }
        static int[] get_cross_winnable_candidates(int[] chessboard) {
            int[][] lines = get_lines();
            List<int> result = new List<int>();
            foreach (int[] line in lines) {
                int circle_count = 0;
                int cross_count = 0;
                int empty_count = 0;
                int empty_pos = -1;
                foreach (int pos in line) {
                    if (is_circle(chessboard[pos])) {
                        ++circle_count;
                    } else if (is_cross(chessboard[pos])) {
                        ++cross_count;
                    } else if (is_empty(chessboard[pos])) {
                        ++empty_count;
                        empty_pos = pos;
                    }
                }
                if (empty_count == 1 && cross_count == 2) {
                    result.Add(empty_pos);
                }
            }
            return result.ToArray();
        }
        static int[] get_circle_winnable_candidates(int[] chessboard) {
            int[][] lines = get_lines();
            List<int> result = new List<int>();
            foreach (int[] line in lines) {
                int circle_count = 0;
                int cross_count = 0;
                int empty_count = 0;
                int empty_pos = -1;
                foreach (int pos in line) {
                    if (is_circle(chessboard[pos])) {
                        ++circle_count;
                    } else if (is_cross(chessboard[pos])) {
                        ++cross_count;
                    } else if (is_empty(chessboard[pos])) {
                        ++empty_count;
                        empty_pos = pos;
                    }
                }
                if (empty_count == 1 && circle_count == 2) {
                    result.Add(empty_pos);
                }
            }
            return result.ToArray();
        }

        static int[] get_corner_candidates(int[] chessboard) {
            int[] corners = {0,2,6,8};
            List<int> result = new List<int>();
            foreach (int corner in corners) {
                if (is_empty(chessboard[corner])) {
                    result.Add(corner);
                }
            }
            return result.ToArray();
        }
        static int[] get_edge_candidates(int[] chessboard) {
            int[] edges = {1,3,5,7};
            List<int> result = new List<int>();
            foreach (int edge in edges) {
                if (is_empty(chessboard[edge])) {
                    result.Add(edge);
                }
            }
            return result.ToArray();
        }
        static int[] get_center_candidates(int[] chessboard) {
            if (is_empty(chessboard[4])) {
                return new int[] {4};
            }
            return new int[] {};
        }
        public static int[] get_candidates(int[] chessboard) {
            int[] candidates = {};
            candidates = get_cross_winnable_candidates(chessboard);
            if (candidates.Length != 0) {
                return candidates;
            }
            candidates = get_circle_winnable_candidates(chessboard);
            if (candidates.Length != 0) {
                return candidates;
            }
            candidates = get_corner_candidates(chessboard);
            if (candidates.Length != 0) {
                return candidates;
            }
            candidates = get_center_candidates(chessboard);
            if (candidates.Length != 0) {
                return candidates;
            }
            candidates = get_edge_candidates(chessboard);
            if (candidates.Length != 0) {
                return candidates;
            }
            return candidates;
        }
        public static int pick_candidate(int[] candidates) {
            if (candidates.Length == 0) {
                return -1;
            }
            return candidates[new Random().Next(0,candidates.Length)];
        }
        static int go(int[] chessboard) {
            int state = get_game_state(chessboard);
            if (state < 0) {
                return state;
            }
            int[] candidates = get_candidates(chessboard);
            int pos = pick_candidate(candidates);
            return pos;
        }
        static String chess_value_to_string(int value) {
            if (is_circle(value)) {
                return "O";
            }
            if (is_cross(value)) {
                return "X";
            }
            return " ";
        }
        public static void print_chessboard(int[] chessboard) {
            var cd = chessboard;
            String f0 = chess_value_to_string(cd[0]);
            String f1 = chess_value_to_string(cd[1]);
            String f2 = chess_value_to_string(cd[2]);
            String f3 = chess_value_to_string(cd[3]);
            String f4 = chess_value_to_string(cd[4]);
            String f5 = chess_value_to_string(cd[5]);
            String f6 = chess_value_to_string(cd[6]);
            String f7 = chess_value_to_string(cd[7]);
            String f8 = chess_value_to_string(cd[8]);
            String[] r0 = {f0, f1, f2};
            String[] r1 = {f3, f4, f5};
            String[] r2 = {f6, f7, f8};
            String line0 = String.Format(" {0} | {1} | {2} ", r0[0], r0[1], r0[2]);
            String line1 = String.Format(" {0} | {1} | {2} ", r1[0], r1[1], r1[2]);
            String line2 = String.Format(" {0} | {1} | {2} ", r2[0], r2[1], r2[2]);
            Console.WriteLine(line0);
            Console.WriteLine("---+---+---");
            Console.WriteLine(line1);
            Console.WriteLine("---+---+---");
            Console.WriteLine(line2);
        }
        public static void print_state(int state) {
            switch (state) {
                case -4: Console.WriteLine("Invalid");    break;
                case -3: Console.WriteLine("Draw");       break;
                case -2: Console.WriteLine("Cross Win");  break;
                case -1: Console.WriteLine("Circle Win"); break;
                default: Console.WriteLine("Playing");    break;
            }
        }
        public static int[] update_chessboard_from_user(int[] chessboard) {
            int pos = Convert.ToInt32(Console.ReadLine());
            int[] result = chessboard;
            pos = pos % 9;
            result[pos] = 1;
            return result;
        }
        public static int[] update_chessboard_from_computer(int[] chessboard) {
            int[] result = chessboard;
            int[] candidates = get_candidates(chessboard);
            int pos = pick_candidate(candidates);
            result[pos] = 2;
            return result;
        }
        static void play_circle_first() {
            int[] chessboard = {0,0,0,0,0,0,0,0,0};
            int state = 0;
            while (true) {

                Console.WriteLine("==== User's term ====");
                chessboard = update_chessboard_from_user(chessboard);
                print_chessboard(chessboard);
                state = get_game_state(chessboard);
                print_state(state);
                if (state < 0)
                    break;
                Console.WriteLine("");

                Console.WriteLine("==== Computer's term ====");
                chessboard = update_chessboard_from_computer(chessboard);
                print_chessboard(chessboard);
                state = get_game_state(chessboard);
                print_state(state);
                if (state < 0)
                    break;
                Console.WriteLine("");
            }

        }
        static void MainOld() 
        {
            int[] chessboard = {0,0,0,0,0,0,0,0,0};
            print_chessboard(chessboard);
            Console.WriteLine("Hello World!");
            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}