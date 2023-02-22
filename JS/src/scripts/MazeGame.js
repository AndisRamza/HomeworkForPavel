"use strict";

(function gameStart() {

    let gameArea = {
        width: 400,
        height: 400,
        canvas: document.createElement("canvas"),
        init: function (){
            this.canvas.width = this.width;
            this.canvas.height = this.height;
            this.canvas.id = "my-game";
            this.context = this.canvas.getContext("2d");

            document.body.insertBefore(this.canvas, document.body.firstChild);
        },
        draw: function (){
            this.context.fillStyle = "black";
            this.context.fillRect(0,0,this.width,this.height);
        },
        clear: function () {
            this.context.clearRect(0,0,this.width,this.height);
        }
    };

    let level = {
        map: [
            [0, 1, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 1, 0, 1, 1, 0, 1, 1, 1, 0],
            [0, 1, 0, 1, 0, 0, 1, 0, 1, 0],
            [0, 1, 1, 1, 0, 0, 1, 0, 1, 0],
            [0, 1, 0, 1, 0, 0, 1, 0, 1, 0],
            [0, 1, 0, 1, 0, 0, 1, 0, 1, 0],
            [0, 1, 0, 1, 0, 0, 1, 0, 1, 0],
            [0, 1, 0, 1, 0, 1, 1, 0, 1, 0],
            [0, 1, 1, 1, 1, 1, 0, 0, 1, 1],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
        ],
        startPosition: [1, 0],
        endPosition: [9, 8],

        mapWallId: 0,
        wallColor: "black",
        mapPathId: 1,
        pathColor: "white",

        init: function(){
            this.context = gameArea.context;
            this.tileHeight = gameArea.height / this.map.length;
            this.tileWidth = gameArea.width / this.map[0].length;
        },
        draw: function(){
            for(let y = 0; y < this.map.length; y++){
                for(let x = 0; x < this.map[0].length; x++){
                    this.changeColorDependingOnTile(x,y);
                    this.context.fillRect(x*this.tileWidth,y*this.tileHeight,this.tileWidth, this.tileHeight);
                    this.context.fill();
                }
            }
        },
        changeColorDependingOnTile: function(x,y){
            if(this.map[y][x] === this.mapWallId){
                this.context.fillStyle = this.wallColor;
            } else {
                this.context.fillStyle = this.pathColor;
            }
        }
    };

    let player = {
        x: 0,
        y: 0,
        color: "red",

        init: function (){
            this.context = gameArea.context;
            this.x = level.startPosition[0];
            this.y = level.startPosition[1];
            this.width = level.tileWidth;
            this.height = level.tileHeight;
        },
        draw: function(){
            this.context.fillStyle = this.color;
            this.context.fillRect(this.x*this.width,this.y*this.height,this.width,this.height);
            this.context.fill();
        },
        move: function(x,y){
            if(this.willCollide(this.x+x,this.y+y)){
            this.changePos(x,y);
            }
        },
        willCollide: function(x,y){
            return level.map[y][x] !== 0;
        },
        changePos: function (x,y){
            this.x += x;
            this.y += y;
        },
        isAtEnd: function(){
            return (level.endPosition[0] === this.x && level.endPosition[1] === this.y);
        }
    };

    let do10TimesPerSecond = setInterval(updateGame, 100);
 
    initGameEntities();
    drawGameEntities();
    registerUserKeyInput();
    createTextElementForPath();

    let start = { x: player.x, y: player.y };
    const end = { x: level.endPosition[0], y: level.endPosition[1] };
    let path = findShortestPath(level.map, start, end);
    
    function updateGame(){
        gameArea.clear();
        level.draw();
        start = { x: player.x, y: player.y };
        path = findShortestPath(level.map, start, end);
        drawPathToCanvas(path);
        player.draw();
        if (player.isAtEnd()){
            finishGame();
        }
    }
    
    function drawPathToCanvas(path){
        let context = gameArea.context;
        context.fillStyle = "yellow";
        path.forEach(cell => {
            context.fillRect(cell.x*level.tileWidth,cell.y*level.tileHeight,level.tileWidth,level.tileHeight);
            context.fill();
        });
    }

    function printTextToTextElement(textElementId, text){
        let textElement = document.getElementById(textElementId);
        textElement.textContent = text;
    }

    function pathToText(){
        let pathText = "";
        path.forEach(path => pathText += `[${path.x} , ${path.y}]`);
        return pathText;
    }

    function createTextElementForPath(){
        let text = document.createElement("p");
        text.id = "my-path-text"
        document.body.insertBefore(text, document.body.childNodes[1]);
    }

    function registerUserKeyInput(){
        window.addEventListener("keydown", doOnUserInput);
    }

    function doOnUserInput(event){
        event.preventDefault();
        movePlayer(event.key);
        removeInputIfGameFinished(event);
    }

    function removeInputIfGameFinished(event){
        if (player.isAtEnd){
            window.removeEventListener("keydown", (event))
        }
    }

    function movePlayer(inputKeys){
        switch(inputKeys){
            case "ArrowUp":
                player.move(0,-1);
                break;
            case "ArrowDown":
                player.move(0,1);
                break;
            case "ArrowLeft":
                player.move(-1,0);
                break;
            case "ArrowRight":
                player.move(1,0);
                break;
            default:
                return;
        }
    }

    function initGameEntities(){
        gameArea.init();
        level.init();
        player.init();
    }

    function drawGameEntities(){
        gameArea.draw();
        level.draw();
        player.draw();
    }
    
    

    function finishGame(){
        clearInterval(do10TimesPerSecond);
        generateWinText();
    }

    function generateWinText(){
        let winText = document.createElement("h1");
        winText.innerHTML = "YOU WIN!";
        document.body.insertBefore(winText, document.body.firstChild);
    }

    
    // Algorithm for shortest path
    function findShortestPath(maze, start, end) {
        const queue = [{ x: start.x, y: start.y, path: [] }];
        const visited = new Set();
      
        while (queue.length > 0) {
          const curr = queue.shift();
      
          if (curr.x === end.x && curr.y === end.y) {
            curr.path.push({ x: curr.x, y: curr.y });
            return curr.path;
          }
      
          if (visited.has(`${curr.x},${curr.y}`)) {
            continue;
          }
      
          visited.add(`${curr.x},${curr.y}`);
      
          for (const [dx, dy] of [[-1, 0], [0, 1], [1, 0], [0, -1]]) {
            const x = curr.x + dx;
            const y = curr.y + dy;
      
            if (isValid(x,y,maze)) { continue; }
      
            const newPath = curr.path.slice();
            newPath.push({ x: curr.x, y: curr.y });
            queue.push({ x, y, path: newPath });
          }
        }
      
        return null;
      }

      function isValid(x,y,maze){
        return (
            x < 0 ||
            y < 0 ||
            x >= maze.length ||
            y >= maze[0].length ||
            maze[y][x] === 0
          );
      }

}());