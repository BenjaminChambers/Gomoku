camera {
	orthographic
	up y
	right x
	location -z*10
	look_at 0
}

sphere {
	0,0.4
	hollow
	
	texture {
		pigment {color transmit 1}
//		finish {specular 1}
	}
	interior {
		media {
			emission rgb 1.1
			absorption rgb 1
			scattering {1, color rgb 7.5}
			density {rgb 10}
		}
	}

	scale <1,1,1/4>
	translate -z
}

light_source {
	<1,1,-1>*10
	color rgb 1
}
