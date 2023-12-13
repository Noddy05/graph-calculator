
float pi = 3.14159265;
vec2 i = vec2(0, 1);
float e = 2.71828183;
float phi = 1.61803398;
float root2 = 1.41421356;
float root5 = 2.23606797;

vec2 Mult(vec2 a, vec2 b){
	return vec2(a.x * b.x - a.y * b.y, a.x * b.y + a.y * b.x);
}


vec2 Div(vec2 a, vec2 b){
	return vec2(a.x / b.x + a.y * b.y, a.y / b.x - a.x / b.y);
}

vec2 EPow(vec2 exponent){
	return pow(e, exponent.x) * vec2(cos(exponent.y), sin(exponent.y));
}

vec2 Mult(vec2 a, vec2 b, float branch){
	float theta1 = atan(a.y, a.x) + 2 * pi * branch;
	float r1 = length(a);
	float theta2 = atan(a.y, a.x) + 2 * pi * branch;
	float r2 = length(a);
	return r1 * r2 * EPow(vec2(0, theta1 + theta2));
}

vec2 Div(vec2 a, vec2 b, float branch){
	float theta1 = atan(a.y, a.x) + 2 * pi * branch;
	float r1 = length(a);
	float theta2 = atan(a.y, a.x) + 2 * pi * branch;
	float r2 = length(a);
	return r1 / r2 * EPow(vec2(0, theta1 - theta2));
}

vec2 Pow(vec2 a, float exponent){
	float theta = atan(a.y, a.x);
	float r = length(a);
	return pow(r, exponent) * vec2(cos(theta * exponent), sin(theta * exponent));
}

vec2 Pow(vec2 a, float exponent, float branch){
	float theta = atan(a.y, a.x) + 2 * pi * branch;
	float r = length(a);
	return pow(r, exponent) * vec2(cos(theta * exponent), sin(theta * exponent));
}

vec2 Log(vec2 a){
	float theta = atan(a.y, a.x);
	float r = length(a);
	return vec2(log(r), theta);
}

vec2 Log(vec2 a, float branch){
	float theta = atan(a.y, a.x) + 2.0 * pi * branch;
	float r = length(a);
	return vec2(log(r), theta);
}

vec2 Pow(float a, vec2 exponent){
	return pow(a, exponent.x) * vec2(cos(log(a) * exponent.y), sin(log(a) * exponent.y));
}
//Pow does not appear to have branches
vec2 Pow(float a, vec2 exponent, float branch){
	return pow(a, exponent.x) * vec2(cos(log(a) * exponent.y), sin(log(a) * exponent.y));
}

vec2 Pow(vec2 base, vec2 exponent){
	float theta = atan(base.y, base.x);
	float r = length(base);
	vec2 rRI = Pow(r, exponent);
	vec2 eRI = EPow(vec2(-exponent.y * theta, exponent.x * theta));

	return Mult(rRI, eRI);
}

vec2 Pow(vec2 base, vec2 exponent, float branch){
	float theta = atan(base.y, base.x) + 2.0 * pi * branch;
	float r = length(base);
	vec2 rRI = Pow(r, exponent);
	vec2 eRI = EPow(vec2(-exponent.y * theta, exponent.x * theta));

	return Mult(rRI, eRI);
}

vec2 MultI(vec2 a){
	return vec2(-a.y, a.x);
}

vec2 Cos(vec2 a){
	return 0.5 * (EPow(MultI(a)) + EPow(-MultI(a)));
}
vec2 Sin(vec2 a){
	return -0.5 * MultI(EPow(MultI(a)) - EPow(-MultI(a)));
}

vec2 Fibonacci(vec2 a){
	return (Pow(phi, a) - Mult(Cos(pi * a), Pow(phi, -a))) / root5;
}

vec2 Zeta(vec2 s){
	vec2 sum = vec2(0);
	if(s.x <= 1.0)
		return vec2(0);
	for(int i = 1; i <= 50; i++){
		sum += Pow(float(i), -s);
	}

	return sum;
}
vec2 AlternatingZeta(vec2 s){
	vec2 sum = vec2(0.0);
	for(int i = 1; i <= 50; i++){
		if(mod(float(i) + 1.0, 2.0) == 0.0)
			sum += Pow(float(i), -s);
		else
			sum -= Pow(float(i), -s);
	}

	return sum;
}

vec2 Factorial(vec2 inputZ)
{
    int N = 30;
	vec2 product = vec2(1, 0);
	for(int i = 1; i <= N; i++){
		product = Mult(product, Pow(vec2(1, 0) + Mult(inputZ, Pow(vec2(float(i), 0), -1.0)), -1.0));
		//product = Div(product, vec2(1, 0) + Div(inputZ, vec2(i, 0)));
	}
	return Mult(Pow(float(N), inputZ), product);
}

vec2 FactorialOld(vec2 inputZ)
{
    float d[11];  
    d[0] = 2.48574089138753565546 * pow(10.0, -5.0);
    d[1] = 1.05142378581721974210;
    d[2] = -3.45687097222016235469;
    d[3] = 4.51227709466894823700;
    d[4] = -2.98285225323576655721;
    d[5] = 1.05639711577126713077;
    d[6] = -1.95428773191645869583 * pow(10.0, -1.0);
    d[7] = 1.70970543404441224307 * pow(10.0, -2.0);
    d[8] = -5.71926117404305781283 * pow(10.0, -4.0);
    d[9] = 4.63399473359905636708 * pow(10.0, -6.0);
    d[10]= -2.71994908488607703910 * pow(10.0, -9.0);
        
    vec2 sum = vec2(d[0], 0);
    for(int i = 1; i < 10; i++)
    {
        sum += d[i] * Pow(inputZ + vec2(i, 0), -1.0);
    }
    vec2 r = 2.0 * sqrt(e / pi) * Mult(Pow((inputZ + vec2(10.9005, 0) + vec2(0.5, 0)) / e, inputZ + vec2(0.5, 0)), sum);
	return r;
}

vec2 DirichletEta(vec2 s){
	if(s.x < 0.0)
		return vec2(0);

	return Mult(Pow(vec2(1, 0) - Pow(2.0, vec2(1, 0) - s), -1.0), AlternatingZeta(s));
}

vec2 ExpandedZeta(vec2 s){
	if(s.x < 0.0)
		return Mult(Mult(Mult(FactorialOld(-s), Pow(2.0 * pi, s - vec2(1.0, 0))) * 2.0, Sin(pi / 2.0 * s)), Zeta(vec2(1, 0) - s));
	return DirichletEta(s);
	//return Pow(2.0, s) * Pow(pi, s - vec2(1, 0)) * Sin(pi * s / 2) * FactorialOld(-s) * Zeta(vec2(1, 0) - s);
}

vec2 RiemannXi(vec2 s){
	return Mult(ExpandedZeta(s), Mult(Pow(pi, - s / 2.0), Mult(s - vec2(1.0, 0),  Factorial(s/2.0 - vec2(1.0, 0)))));
}