// +w40 +h40 +fn +ua

camera {
	orthographic
	up y
	right x
	location -z*10
	look_at 0
}

sphere {
	0,0.4
	scale <1,1,1/4>
	translate -z
	
	texture {
		pigment {color rgb 0}
		finish {specular 1}
	}
}

light_source {
	<1,1,-1>*10
	color rgb 1
}
