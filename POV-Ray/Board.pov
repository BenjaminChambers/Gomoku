// +w600 +h600 +fn +ua

camera {
	orthographic
	up y*15
	right x*15
	location -z*10
	look_at 0
}

#declare lineThickness = 0.025;

#for (idx, 0, 14, 1)
	cylinder {0,14*x, lineThickness translate y*idx translate -7*<1,1,0>}
	cylinder {0,14*y, lineThickness translate x*idx translate -7*<1,1,0>}
#end

sphere {<-7,-7,0>,lineThickness}
sphere {<-7, 7,0>,lineThickness}
sphere {< 7,-7,0>,lineThickness}
sphere {< 7, 7,0>,lineThickness}

background {color rgb <5/8,3/8,0>}
