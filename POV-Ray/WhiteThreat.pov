camera {
	orthographic
	up y
	right x
	location -z*10
	look_at 0
}

#declare bar = merge {
	cylinder {x*0.25, x*0.45, 0.05}
	sphere {x*0.25, 0.05}
	sphere {x*0.45, 0.05}
}
	
merge {
	torus {0.35, 0.05}
	object {bar}
	object {bar rotate y*90}
	object {bar rotate y*180}
	object {bar rotate y*270}
	
	hollow
	
	texture {
		pigment {color transmit 1}
	}
	interior {
		media {
			emission rgb 1.1
			absorption rgb 1
			scattering {1, color rgb 7.5}
			density {rgb 10}
		}
	}
	
	rotate x*90
}

light_source {
	<1,1,-1>*10
	color rgb 1
}
