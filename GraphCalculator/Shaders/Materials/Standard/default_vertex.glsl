    
    #version 330 core

    layout (location = 0) in vec3 position;

    #libs

    uniform mat4 transform;
    uniform mat4 perspective;
    uniform mat4 camera;
    out vec3 vPosition;
    out vec2 functionValue;


    void main() {
        vPosition = position;
        vec2 function = ExpandedZeta(vec2(position.x, position.y) * 2 * 50);
        functionValue = function;
        gl_Position = perspective * camera * transform * vec4(position - 0.025 * vec3(0, 0, 
            length(function)), 1);
    }


