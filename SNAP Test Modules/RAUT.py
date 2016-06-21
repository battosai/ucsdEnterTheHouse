from framework.latentmodule import LatentModule

class Main(LatentModule):
    def __init__(self):
        LatentModule.__init__(self)

        self.ratWait = 5                    # this will be the wait time for each trio in the RAT
        self.ratRounds = 3                  # number of word trios to be presented (number of elements in ratGroup)
        self.ratGroup = ["COTTAGE SWISS CAKE", "LOSER THROAT SPOT", "SHOW LIFE ROW"]

        self.autWait = 10                   # this will be the wait time for each object in the AUT
        self.autRounds = 2                  # number of objects to be presented (number of elements in autGroup)
        self.autGroup = ["BRICK", "BUTTON"]

    def run(self):
        self.marker(10)
        self.write('In this experiment, you will be shown a trio of words.\nYour task is to find the fourth word that the trio share.\nPress the space bar to continue.', 'space')
        self.write('For example, if you were shown CREAM SKATE CUBE...\nYou would want to answer with ICE.', 'space')
        self.write('You will be given %d seconds for each trio.\nIf you can not come up with the answer in time, move on.' %self.ratWait, 'space')
        self.write('Press the space bar when you are ready to begin.', 'space')

        #RAT
        for i in range(self.ratRounds):
            self.marker(20 + i)
            self.write(self.ratGroup[i], self.ratWait, scale = 0.2)
            self.sleep(2)

        self.write('You have finished the first portion of the experiment.', 5)

        self.write('In the next experiment, you will be told/shown a random object.\nYour task is to come up with as many uses for the object as you can.\nPress the space bar to continue.', 'space')
        self.write('You will be given %d seconds for each object.\nPress the space bar when you are ready to begin' %self.autWait, 'space')

        #AUT
        for i in range(self.autRounds):
            self.marker(40 + i)
            self.write(self.autGroup[i], self.autWait, scale = 0.5)
            self.sleep(2)

        self.write('You have finished the experiment. Thank you.', 5)