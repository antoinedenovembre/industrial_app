#include <iostream>
#include <fstream>
#include <sstream>
#include <string>
#include <windows.h>
#include <cmath>
#include <vector>
#include <ctime>
#include <stack>

#include "libIHM.h"

ClibIHM::ClibIHM() 
{
	this->nbDataImg = 0;
	this->dataFromImg.clear();
	this->imgPt = NULL;
}

ClibIHM::ClibIHM(int nbChamps, byte* data, int stride, int nbLig, int nbCol)
{
	this->nbDataImg = nbChamps;
	this->dataFromImg.resize(nbChamps);

	this->imgPt = new CImageCouleur(nbLig, nbCol);
	CImageCouleur out(nbLig, nbCol);

	// On copie les pixels de la bitmap
	byte* pixPtr = (byte*)data;

	for (int y = 0; y < nbLig; y++)
	{
		for (int x = 0; x < nbCol; x++)
		{
			this->imgPt->operator()(y, x)[0] = pixPtr[3 * x + 2];
			this->imgPt->operator()(y, x)[1] = pixPtr[3 * x + 1];
			this->imgPt->operator()(y, x)[2] = pixPtr[3 * x ];
		}
		pixPtr += stride; // Largeur une seule ligne gestion multiple 32 bits
	}

	CImageNdg imgNdg = this->imgPt->plan();
	int seuilPlaceholder = 0;
	CImageNdg imgSeuillee = this->imgPt->plan().seuillage("automatique", seuilPlaceholder, seuilPlaceholder);

	for (int i = 0; i < imgSeuillee.lireNbPixels(); i++)
	{
		out(i)[0] = (unsigned char)(255*(int)imgSeuillee(i));
		out(i)[1] = 0;
		out(i)[2] = 0;
	}

	pixPtr = (byte*)data;
	for (int y = 0; y < nbLig; y++)
	{
		for (int x = 0; x < nbCol; x++)
		{
			pixPtr[3 * x + 2] = out(y, x)[0];
			pixPtr[3 * x + 1] = out(y, x)[1];
			pixPtr[3 * x] = out(y, x)[2];
		}
		pixPtr += stride; // largeur une seule ligne gestion multiple 32 bits
	}
}

int ClibIHM::calibImage()
{
	if (this->imgPt == NULL)
	{
		return -1;
	}

	CImageNdg imgNdg = this->imgPt->plan();

	// Calcul médiane
	int mediane = (int) imgNdg.mediane();

	return mediane;
}

ClibIHM::~ClibIHM() {
	
	if (imgPt)
		(*this->imgPt).~CImageCouleur(); 
	this->dataFromImg.clear();
}