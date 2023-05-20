from PIL import Image

# Define the size of the grid
ROWS = 17
COLS = 33
SQUARE_WIDTH = 1
SQUARE_HEIGHT = 1

# Define the palette of 70+ colors
PALETTE = [
    (56, 56, 56), #Blank
    (200, 0, 16), #Red
    (255, 140, 0), #Orange
    (255, 239, 0), #Yellow
    (32, 255, 0), #Green   
    (0, 76, 255), #Blue
    (71, 0, 255), #Indigo
    (170, 0, 178), #Voilet
    (46, 34, 47), #2e222f
    (62, 53, 70), #3e3546
    (98, 85, 101), #625565
    (150, 108, 108), #966c6c
    (171, 148, 122), #ab947a
    (105, 79, 98), #694f62
    (127, 112, 138), #7f708a
    (155, 171, 178), #9babb2
    (199, 220, 208), #c7dcd0
    (255, 255, 255), #ffffff
    (110, 39, 39), #6e2727
    (179, 56, 49), #b33831
    (234, 79, 54), #ea4f36
    (245, 125, 74), #f57d4a
    (174, 35, 52), #ae2334
    (232, 59, 59), #e83b3b
    (251, 107, 29), #fb6b1d
    (247, 150, 23), #f79617
    (249, 194, 43), #f9c22b
    (122, 48, 69), #7a3045
    (158, 69, 57), #9e4539
    (205, 104, 61), #cd683d
    (230, 144, 78), #e6904e
    (251, 185, 84), #fbb954
    (76, 62, 36), #4c3e24
    (103, 102, 51), #676633
    (162, 169, 71), #a2a947
    (213, 224, 75), #d5e04b
    (251, 255, 134), #fbff86
    (22, 90, 76), #165a4c
    (35, 144, 99), #239063
    (30, 188, 115), #1ebc73
    (145, 219, 105), #91db69
    (205, 223, 108), #cddf6c
    (49, 54, 56), #313638
    (55, 78, 74), #374e4a
    (84, 126, 100), #547e64
    (146, 169, 132), #92a984
    (178, 186, 144), #b2ba90
    (11, 94, 101), #0b5e65
    (11, 138, 143), #0b8a8f
    (14, 175, 155), #0eaf9b
    (45, 255, 185), #30e1b9
    (143, 248, 226), #8ff8e2
    (50, 51, 83), #323353
    (72, 74, 119), #484a77
    (77, 101, 180), #4d65b4
    (77, 155, 230), #4d9be6
    (143, 211, 255), #8fd3ff
    (69, 41, 63), #45293f
    (107, 62, 117), #6b3e75
    (144, 94, 169), #905ea9
    (168, 132, 243), #a884f3
    (234, 173, 237), #eaaded
    (117, 60, 84), #753c54
    (162, 75, 111), #a24b6f
    (207, 101, 127), #cf657f
    (237, 128, 153), #ed8099
    (131, 28, 93), #831c5d
    (195, 36, 84), #c32454
    (240, 79, 120), #f04f78
    (246, 129, 129), #f68181
    (252, 167, 144), #fca790
    (253, 203, 176), #fdcbb0
]

# Load the PNG file
image = Image.open("level.png")

# Convert the image to RGB mode
image = image.convert("RGB")

# Calculate the size of each square in pixels
square_width_px = SQUARE_WIDTH * image.width // COLS
square_height_px = SQUARE_HEIGHT * image.height // ROWS

# Create a new grid data structure to store the color indices
grid = [[0 for j in range(COLS)] for i in range(ROWS)]

# Convert each pixel color to a color index using the lookup table
for i in range(ROWS):
    for j in range(COLS):
        x = j * square_width_px
        y = i * square_height_px
        color = image.getpixel((x, y))
        index = PALETTE.index(color)
        grid[i][j] = index

# Export the grid data to a text file
with open("Level_0000C.txt", "w") as file:
    for row in grid:
        line = ",".join(str(color) for color in row) + "\n"
        file.write(line)
    file.write("--")  # Add "--" as the final line

# Create a new grid with 0 for the first number in the palette list and 1 for all other values
grid_modified = [[0 if color == 0 else 1 for color in row] for row in grid]

# Create a new grid with 0 for the first number in the palette list and 1 for all other values
grid_modified4 = [[0 if color == 0 else 4 for color in row] for row in grid]

# Export the modified grid data to a text file
with open("Level_0000.txt", "w") as file:
    for row in grid_modified:
        line = ",".join(str(color) for color in row) + "\n"
        file.write(line)
    file.write("--")  # Add "--" as the final line
        
# Export the modified grid data to a text file
with open("Level_0000S.txt", "w") as file:
    for row in grid_modified4:
        line = ",".join(str(color) for color in row) + "\n"
        file.write(line)
    file.write("--")  # Add "--" as the final line

