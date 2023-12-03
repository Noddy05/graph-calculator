#version 330 core

uniform vec3 scale = vec3(1, 1, 0);
in vec3 vPosition;
out vec4 color;

float function(float x){
    return 1 + x / (pow(2, x) - 1) / (x - 1);
}

bool underCurve(float x, float y){ //Returns wether y < f(x)
    return y < function(x);
}

bool underCurveTwoSided(float x, float y){ //Returns true if y is between 0 and f(x) 
    return y * y < function(x) * y;
}

float lerp(float t, float a, float b){
    return a + (b - a) * t;
}

float map(float t, float a, float b, float x, float y){
    return x + (t - a) / (b - a) * (y - x);
}

void main() {
    float xyRatio = scale.y / scale.x;

    //Graph parameters:
    float minX = -1; float maxX = 10;
    float minY = -1; float maxY = 10;

    //For graphing:
    float x = map(vPosition.x, -0.5, 0.5, minX, maxX);
    float y = map(vPosition.y, -0.5, 0.5, minY, maxY);

    //Settings:
    color = vec4(1, 1, 1, 1); //Set a background color
    vec3 axisColor = vec3(0, 0, 0); //Color of the x,y-axes
    float axisThickness = 0.05; //How thick should the x,y-axes be

    vec3 primaryGridColor = vec3(0.8, 0.8, 0.8); //Color of the background grid
    float primaryGridSize = 1; //Draw a background grid at this scale
    float primaryGridThickness = 0.03; //How thick should the background grid be

    vec3 secondaryGridColor = vec3(0.9, 0.9, 0.9); //Color of the background grid
    float secondaryGridSize = 0.2; //Draw a background grid at this scale
    float secondaryGridThickness = 0.02; //How thick should the background grid be

    //Draw graph background:

    //Secondary grid:
    if(abs(mod(x, secondaryGridSize)) <= secondaryGridThickness){
        color = vec4(secondaryGridColor, 1);
    }
    if(abs(mod(y, secondaryGridSize)) <= secondaryGridThickness * xyRatio){
        color = vec4(secondaryGridColor, 1);
    }
    //Primary grid:
    if(abs(mod(x, primaryGridSize)) <= primaryGridThickness){
        color = vec4(primaryGridColor, 1);
    }
    if(abs(mod(y, primaryGridSize)) <= primaryGridThickness * xyRatio){
        color = vec4(primaryGridColor, 1);
    }

    //Axes:
    if(abs(x) <= axisThickness){
        color = vec4(axisColor, 1);
    }
    if(abs(y) <= axisThickness * xyRatio){
        color = vec4(axisColor, 1);
    }

    //Draw function:
    float functionYValue = function(x);

    //Drawing area under curve
    if(underCurveTwoSided(x, y)){
        color += vec4(0.65, 0.22, 0.5, 0.1);
    }

    float checkGridSize = 0.01;
    int checkGridDetail = 5;
    bool aboveGraph = false;
    bool belowGraph = false;
    for(int xOffset = 0; xOffset < checkGridDetail * 2 + 1; xOffset++){
        for(int yOffset = 0; yOffset < checkGridDetail * 2 + 1; yOffset++){
            if((pow(x + (xOffset - checkGridDetail), 2) >= pow((checkGridDetail), 2)
                || pow(y + (yOffset - checkGridDetail), 2) >= pow((checkGridDetail), 2))){
                continue;
            }
            if(underCurve(x + (xOffset - checkGridDetail) * checkGridSize, 
                y + (yOffset - checkGridDetail) * checkGridSize)){
                belowGraph = true;
            } else {
                aboveGraph = true;
            }
        }
    }
    if(belowGraph && aboveGraph){
        color = vec4(0, 0, 0, 1);
    }


    /*
    float domainSize = 2;
    float rangeSize = 0.1;
    float x = vPosition.x / scale.x; float y = vPosition.y / scale.y + 0.5;
    //Set background color
    color = vec4(0.9f, 0.9f, 0.9f, 1f);

    //Draw under curve
    vec3 colorUnderCurve = vec3(0.6, 0.6, 0.6);
    if(underCurve(x * domainSize, y * rangeSize) && y >= 0 && x * domainSize > 1) {
        color = vec4(colorUnderCurve, 1f);
    }

    //Draw grid
    float cellSize = 0.5; //For every x draw a line
    float gridWidth = 0.003;
    vec3 gridColor = vec3(0.4, 0.4, 0.4);
    if(abs(mod(x, cellSize)) <= gridWidth){
        color = vec4(gridColor, 1);
    }
    if(abs(mod(y, cellSize)) <= gridWidth){
        color = vec4(gridColor, 1);
    }

    float axisThickness = 0.005;
    vec3 axisColor = vec3(0.1, 0.1, 0.1);
    if(abs(x) <= axisThickness){
        color = vec4(axisColor, 1);
    }
    if(abs(y) <= axisThickness){
        color = vec4(axisColor, 1);
    }

    //Draw curve
    int size = 3;
    float sizeMultiplier = 0.02;
    vec3 curveColor = vec3(0.1, 0.2, 0.9);
    int outside = 0;
    int inside = 0;
    for(int x2 = 0; x2 < size; x2++){
        for(int y2 = 0; y2 < size; y2++){
            if(underCurve((x2 / float(size) - 0.5) * sizeMultiplier * domainSize + x * domainSize, 
                (y2 / float(size) - 0.5) * sizeMultiplier * rangeSize + y * rangeSize)){
                inside++;
            } else {
                outside++;
            }
        }
    }
    if(outside > 0 && inside > 0){
        color = vec4(curveColor, 1);
    }
    */
}